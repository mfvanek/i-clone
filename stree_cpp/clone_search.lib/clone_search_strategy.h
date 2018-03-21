#pragma once

#include "..\cpp_event.lib\CppEvent.h"
#include "common_pointers.h"
#include "ILanguageExtension.h"
#include "filesystem.h"
#include "date_time.h"
#include "supermax_node.h"
#include <string>

namespace clone_search
{
   class CBaseCloneSearchStrategy
   {
   public:
      typedef DefSmartPtr<CBaseCloneSearchStrategy>::Ptr Ptr;

      class InputParams
      {
      public:
         InputParams(const std::string& _directory, const std::string& _extensions, source_files::LANGUAGES _language, int _K_min, const std::string& _skipped_files);
         std::string directory;
         std::string extensions;
         source_files::LANGUAGES language;
         int K_min;
         std::string skipped_files;
      };

      class OutputParams
      {
      public:
         source_files::CSourceFileID::Collection files_id;
         source_files::CCodeUnitsCollection::Type code_units;
         supermax_node::nodes clones;
         temporal::CTimeSpan time_elapsed;
      };

   private:
      void OnBeginFilesListBuilding();
      void OnEndFilesListBuilding();

      void OnBeginCodeUnitsListBuilding();
      void OnEndCodeUnitsListBuilding();

      void OnBeginSearchClones();
      void OnEndSearchClones();

      void OnBeginFilterIrrelevantClones();
      void OnEndFilterIrrelevantClones();

      void OnBeginSortClones();
      void OnEndSortClones();

   private:
      bool is_belong_one_file(const supermax_node& node, int position) const;
      bool is_overlapping(const supermax_node& node) const;
      bool is_irrelevant(const supermax_node& node) const;
      int get_ending(const supermax_node& node, int position) const;

   protected:
      InputParams m_in;
      source_files::ILanguageExtension::Ptr m_tokenizer;
      filesystem::files_collection m_files;
      OutputParams m_out;

      virtual void build_files_list();
      virtual void build_code_units_list();
      virtual void filter_irrelevant_clones();
      virtual void sort_clones();

   public:
      CBaseCloneSearchStrategy(const InputParams& input);
      virtual ~CBaseCloneSearchStrategy() {}

      CppEvent1<bool, std::string> search_event;
      void FindClones();
      void PrintClones() const;
   };
}