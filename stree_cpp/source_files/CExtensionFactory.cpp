#include "CExtensionFactory.h"
#include "Scanner.h"
#include "CsScanner.h"
#include <map>
#include <algorithm>
#include <functional>
#include <cctype>

namespace source_files
{
   class CppExtension : public ILanguageExtension
   {
   public:
      virtual void Tokenize(CTokenizeResult& tokens);
   };

   static CCodeUnit FromToken(const Coco::Token* token, const CSourceFileID::Ptr& file_id)
   {
      std::string val = Coco::string_create_char(token->val);
      CElementPosition pos(token->line, token->col, token->line, token->col + val.size());
      CCodeUnit unit(file_id, pos, val);
      return unit;
   }

   void CppExtension::Tokenize(CTokenizeResult& tokens)
   {
      Coco::Cpp::Scanner lex(tokens.GetSourceFileName());
      Coco::Token* token = lex.Scan();
      while (token != nullptr && token->kind != 0)
      {
         tokens.GetCollection().push_back(FromToken(token, tokens.GetSourceFileID()));
         token = lex.Scan();
      }
   }

   class CsExtension : public ILanguageExtension
   {
   public:
      virtual void Tokenize(CTokenizeResult& tokens);
   };

   void CsExtension::Tokenize(CTokenizeResult& tokens)
   {
      Coco::Cs::Scanner lex(tokens.GetSourceFileName());
      Coco::Token* token = lex.Scan();
      while (token != nullptr && token->kind != 0)
      {
         tokens.GetCollection().push_back(FromToken(token, tokens.GetSourceFileID()));
         token = lex.Scan();
      }
   }

   ILanguageExtension::Ptr CExtensionFactory::Create(LANGUAGES Language)
   {
      switch(Language)
      {
      case LANGUAGE_C_PLUS_PLUS:
         return ILanguageExtension::Ptr(new CppExtension());

      case LANGUAGE_C_SHARP:
         return ILanguageExtension::Ptr(new CsExtension());

      case LANGUAGE_JAVA:
         break;
      }
      throw std::logic_error("not implemented");
   }

   static std::string to_upper(const std::string& input)
   {
      std::string output(input);
      std::transform(output.begin(), output.end(), output.begin(), std::toupper);
      return output;
   }

   LANGUAGES CExtensionFactory::GetLanguage(const std::string& Language)
   {
      std::string lang = to_upper(Language);
      static std::map<std::string, LANGUAGES> lang_table;
      std::map<std::string, LANGUAGES>::const_iterator it;
      if(lang_table.empty())
      {
         lang_table["2"] = LANGUAGE_C_PLUS_PLUS;
         lang_table["CPP"] = LANGUAGE_C_PLUS_PLUS;
         lang_table["C++"] = LANGUAGE_C_PLUS_PLUS;
         lang_table["CPLUSPLUS"] = LANGUAGE_C_PLUS_PLUS;
         lang_table["1"] = LANGUAGE_C_SHARP;
         lang_table["CS"] = LANGUAGE_C_SHARP;
         lang_table["C#"] = LANGUAGE_C_SHARP;
         lang_table["CSHARP"] = LANGUAGE_C_SHARP;
      }
      it = lang_table.find(lang);
      if(it == lang_table.end())
         throw std::exception("unknown language");
      return it->second;
   }

   ILanguageExtension::Ptr CExtensionFactory::Create(const std::string& Language)
   {
      LANGUAGES Lang = GetLanguage(Language);
      return Create(Lang);
   }
}