#include "repeats_supermax_template.h"
#include "clone_search_strategy.h"
#include "CExtensionFactory.h"
#include "clone_print_strategy.h"
#include <sstream>

namespace clone_search
{
   CBaseCloneSearchStrategy::InputParams::InputParams(const std::string& _directory, const std::string& _extensions, source_files::LANGUAGES _language, int _K_min, const std::string& _skipped_files)
      : directory(_directory), extensions(_extensions), language(_language), K_min(_K_min), skipped_files(_skipped_files)
   {
      if(_directory.empty() || _extensions.empty() || _K_min <= 0)
         throw std::invalid_argument("Error in clone search parametres");
   }

   CBaseCloneSearchStrategy::CBaseCloneSearchStrategy(const InputParams& input)
      : m_in(input), m_tokenizer(source_files::CExtensionFactory::Create(input.language))
   {}

   void CBaseCloneSearchStrategy::FindClones()
   {
      temporal::CTimer timer;
      timer.Start();

      OnBeginFilesListBuilding();
      build_files_list();
      OnEndFilesListBuilding();

      OnBeginCodeUnitsListBuilding();
      build_code_units_list();
      OnEndCodeUnitsListBuilding();

      OnBeginSearchClones();
      supermax_find(m_out.code_units, m_in.K_min, m_out.clones);
      OnEndSearchClones();

      OnBeginFilterIrrelevantClones();
      filter_irrelevant_clones();
      OnEndFilterIrrelevantClones();

      OnBeginSortClones();
      sort_clones();
      OnEndSortClones();

      timer.Stop();
      m_out.time_elapsed = timer.GetSpan();
   }

   void CBaseCloneSearchStrategy::OnBeginFilesListBuilding()
   {
      std::stringstream sst;
      sst << "Start searching source files in \"" << m_in.directory.c_str() << "\" with extensions = \"" << m_in.extensions.c_str() << "\".";
      search_event.notify(sst.str());
   }

   void CBaseCloneSearchStrategy::OnEndFilesListBuilding()
   {
      std::stringstream sst;
      sst << "End searching. " << m_files.size() << " source files found.";
      search_event.notify(sst.str());
   }

   void CBaseCloneSearchStrategy::OnBeginCodeUnitsListBuilding()
   {
      search_event.notify("Start building code units sequence over searched source files.");
   }

   void CBaseCloneSearchStrategy::OnEndCodeUnitsListBuilding()
   {
      std::stringstream sst;
      sst << "End building sequence. " << m_out.code_units.size() << " code units found.";
      search_event.notify(sst.str());
   }

   void CBaseCloneSearchStrategy::OnBeginSearchClones()
   {
      std::stringstream sst;
      sst << "Start searching clones on code units sequence with size threshold = " << m_in.K_min << ".";
      search_event.notify(sst.str());
   }

   void CBaseCloneSearchStrategy::OnEndSearchClones()
   {
      std::stringstream sst;
      sst << "End searching. " << m_out.clones.size() << " clone occurrences found.";
      search_event.notify(sst.str());
   }

   void CBaseCloneSearchStrategy::OnBeginFilterIrrelevantClones()
   {
      std::stringstream sst;
      sst << "Start filtering irrelevant and overlapping clones";
      search_event.notify(sst.str());
   }

   void CBaseCloneSearchStrategy::OnEndFilterIrrelevantClones()
   {
      std::stringstream sst;
      sst << "End filtering. " << m_out.clones.size() << " clone occurrences remained.";
      search_event.notify(sst.str());
   }

   void CBaseCloneSearchStrategy::OnBeginSortClones()
   {
      std::stringstream sst;
      sst << "Start sorting clones";
      search_event.notify(sst.str());
   }

   void CBaseCloneSearchStrategy::OnEndSortClones()
   {
      std::stringstream sst;
      sst << "End sorting clones";
      search_event.notify(sst.str());
   }

   void CBaseCloneSearchStrategy::build_files_list()
   {
      m_files = filesystem::get_files(m_in.directory, m_in.extensions, m_in.skipped_files);
   }

   void CBaseCloneSearchStrategy::build_code_units_list()
   {
      for(filesystem::files_collection::const_iterator it = m_files.begin(); it != m_files.end(); ++it)
      {
         source_files::CTokenizeResult tokens(*it);
         m_tokenizer->Tokenize(tokens);
         m_out.files_id.push_back(tokens.GetSourceFileID());
         m_out.code_units.insert(m_out.code_units.end(), tokens.GetCollection().begin(), tokens.GetCollection().end());
      }
   }

   int CBaseCloneSearchStrategy::get_ending(const supermax_node& node, int position) const
   {
      return node.get_ending(position);
   }

   bool CBaseCloneSearchStrategy::is_belong_one_file(const supermax_node& node, int position) const
   {
      long file_id_begin = m_out.code_units[position].GetSourceFileID()->GetSourceFileID();
      long file_id_end = m_out.code_units[get_ending(node, position)].GetSourceFileID()->GetSourceFileID();
      return (file_id_begin == file_id_end);
   }

   bool CBaseCloneSearchStrategy::is_irrelevant(const supermax_node& node) const
   {
      for(supermax_node::PositionsIter it = node.pos.begin(); it != node.pos.end(); ++it)
      {
         if(!is_belong_one_file(node, *it))
            return true;
      }
      return false;
   }

   bool CBaseCloneSearchStrategy::is_overlapping(const supermax_node& node) const
   {
      supermax_node::Positions sorted_pos(node.pos);
      std::sort(sorted_pos.begin(), sorted_pos.end());
      for(size_t counter = 1; counter < sorted_pos.size(); ++counter)
      {
         if(sorted_pos[counter] <= get_ending(node, sorted_pos[counter - 1]))
            return true;
      }
      return false;
   }

   void CBaseCloneSearchStrategy::filter_irrelevant_clones()
   {
      for(supermax_node::nodes::iterator it = m_out.clones.begin(); it != m_out.clones.end(); ++it)
      {
         if(is_overlapping(*it) || is_irrelevant(*it))
            it = m_out.clones.erase(it);
      }
   }

   struct supermax_node_less
   {
      bool operator()(const supermax_node& lhs, const supermax_node& rhs)
      {
         // Сортируем по убыванию
         return lhs.M > rhs.M;
      }
   };

   void CBaseCloneSearchStrategy::sort_clones()
   {
      m_out.clones.sort(supermax_node_less());
   }

   void CBaseCloneSearchStrategy::PrintClones() const
   {
      CCloneReportPrinter printer(m_in, m_out);
      printer.Print();
   }
}