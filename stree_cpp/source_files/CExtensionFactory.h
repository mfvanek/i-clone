#pragma once

#include "ILanguageExtension.h"
#include <string>

namespace source_files
{
   class CExtensionFactory
   {
   public:
      static ILanguageExtension::Ptr Create(LANGUAGES Language);
      static ILanguageExtension::Ptr Create(const std::string& Language);
      static LANGUAGES GetLanguage(const std::string& Language);
   };
}