#include "clone_print_strategy.h"
#include <fstream>
#include "date_time.h"

namespace clone_search
{
   CCloneReportPrinter::CCloneReportPrinter(const CBaseCloneSearchStrategy::InputParams& _search_parm, const CBaseCloneSearchStrategy::OutputParams& _search_result)
      : search_parm(_search_parm), search_result(_search_result)
   {}

   void CCloneReportPrinter::print_clone(std::fstream& clone_report, const supermax_node& node) const
   {
      clone_report << std::endl;
      clone_report << "Дублируются фрагменты размером " << node.M << " единиц кода" << std::endl;
      for(supermax_node::PositionsIter it = node.pos.begin(); it != node.pos.end(); ++it)
      {
         source_files::CCodeUnit begining = search_result.code_units[*it];
         source_files::CCodeUnit ending = search_result.code_units[node.get_ending(*it)];
         std::string filename = begining.GetSourceFileID()->GetSourceFileName();
         clone_report << "Файл: " << filename.c_str() << std::endl;
         clone_report << "Начало клон-фрагмента: строка " << begining.GetLineStart() << ", позиция " << begining.GetIndexStart() << std::endl;
         clone_report << "Конец клон-фрагмента: строка " << ending.GetLineEnd() << ", позиция " << ending.GetIndexEnd()  << std::endl;
      }
   }

   void CCloneReportPrinter::Print() const
   {
      std::string report_filename = filesystem::Path::Combine(search_parm.directory, "iclone_report.txt");
      std::fstream clone_report(report_filename, std::fstream::out);
      clone_report << "Отчет о поиске программных клонов" << std::endl << std::endl;
      clone_report << "Дата выполнения процедуры: " << temporal::CTimeStamp::Now().ToString() << std::endl;
      clone_report << "Минимальный размер клон-фрагмента: " << search_parm.K_min << std::endl;
      clone_report << "Обработано файлов: " << search_result.files_id.size() << std::endl;
      clone_report << "Обработано единиц кода: " << search_result.code_units.size() << std::endl;
      clone_report << "Обнаружено случаев дублирования кода: " << search_result.clones.size() << std::endl;
      clone_report << "Суммарное время выполнения: " << search_result.time_elapsed.ToString() << std::endl;
      long cloned_code_units_amount = 0;
      supermax_node::nodes::const_iterator it = search_result.clones.begin();
      for(; it != search_result.clones.end(); ++it)
      {
         cloned_code_units_amount += it->M * it->num_witness;
         print_clone(clone_report, *it);
      }
      double M_clones = (double)cloned_code_units_amount/(double)search_result.code_units.size();
      clone_report << std::endl << "Метрика дублированности: " << M_clones;
   }
}