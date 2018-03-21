#pragma once

#include "clone_search_strategy.h"

namespace clone_search
{
   class CCloneReportPrinter
   {
   private:
      CCloneReportPrinter(const CCloneReportPrinter&);
      CCloneReportPrinter& operator=(const CCloneReportPrinter&);

   protected:
      const CBaseCloneSearchStrategy::InputParams& search_parm;
      const CBaseCloneSearchStrategy::OutputParams& search_result;
      virtual void print_clone(std::fstream& clone_report, const supermax_node& node) const;

   public:
      CCloneReportPrinter(const CBaseCloneSearchStrategy::InputParams& _search_parm, const CBaseCloneSearchStrategy::OutputParams& _search_result);
      virtual ~CCloneReportPrinter() {}
      virtual void Print() const;
   };
}