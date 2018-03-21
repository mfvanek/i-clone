#pragma once

#include "coco_common.h"

namespace Coco
{
   namespace Cs
   {
      class Scanner
      {
      private:
         static const unsigned char EOL = '\n';
         static const int eofSym = 0;
         static const int maxT = 142;
         static const int noSym = 142;

         void *firstHeap;
         void *heap;
         void *heapTop;
         void **heapEnd;

         StartStates start;
         KeywordMap keywords;

         Token *t;         // current token
         wchar_t *tval;    // text of current token
         int tvalLength;   // length of text of current token
         int tlen;         // length of current token

         Token *tokens;    // list of tokens already peeked (first token is a dummy)
         Token *pt;        // current peek token

         int ch;           // current input character
         int pos;          // byte position of current character
         int charPos;      // position by unicode characters starting with 0
         int line;         // line number of current character
         int col;          // column number of current character
         int oldEols;      // EOLs that appeared in a comment;

         void CreateHeapBlock();
         Token* CreateToken();
         void AppendVal(Token *t);
         void SetScannerBehindT();

         void Init();
         void NextCh();
         void AddCh();
         bool Comment0();
         bool Comment1();
         Token* NextToken();

      public:
         Buffer *buffer;   // scanner buffer

         Scanner(const unsigned char* buf, int len);
         Scanner(const std::string& fileName);
         Scanner(FILE* s);
         ~Scanner();
         Token* Scan();
         Token* Peek();
         void ResetPeek();
      };
   }
}