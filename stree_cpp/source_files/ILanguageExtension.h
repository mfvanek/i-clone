#pragma once

#include "CTokenizeResult.h"
#include "common_pointers.h"

namespace source_files
{
   enum LANGUAGES
   {
      LANGUAGE_C_SHARP = 1,
      LANGUAGE_C_PLUS_PLUS,
      LANGUAGE_JAVA
   };

   class ILanguageExtension
   {
   public:
      typedef DefSmartPtr<ILanguageExtension>::Ptr Ptr;
      virtual ~ILanguageExtension() {}
      virtual void Tokenize(CTokenizeResult& tokens) = 0;
   };
}