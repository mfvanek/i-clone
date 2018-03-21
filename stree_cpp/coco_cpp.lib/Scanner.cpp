#include <memory.h>
#include <string.h>
#include "Scanner.h"
#include <stdexcept>

namespace Coco
{
   namespace Cpp
   {
      Scanner::Scanner(const unsigned char* buf, int len)
      {
         buffer = new Buffer(buf, len);
         Init();
      }

      Scanner::Scanner(const std::string& fileName)
      {
         FILE* stream;
         if ((stream = fopen(fileName.c_str(), "rb")) == NULL)
         {
            throw std::invalid_argument(fileName);
         }
         buffer = new Buffer(stream, false);
         Init();
      }

      Scanner::Scanner(FILE* s)
      {
         buffer = new Buffer(s, true);
         Init();
      }

      Scanner::~Scanner() {
         char* cur = (char*) firstHeap;

         while(cur != NULL) {
            cur = *(char**) (cur + COCO_HEAP_BLOCK_SIZE);
            free(firstHeap);
            firstHeap = cur;
         }
         delete [] tval;
         delete buffer;
      }

#pragma warning(disable: 4127)
      void Scanner::Init()
      {
         int i;
         for (i = 65; i <= 90; ++i) start.set(i, 1);
         for (i = 95; i <= 95; ++i) start.set(i, 1);
         for (i = 97; i <= 122; ++i) start.set(i, 1);
         for (i = 48; i <= 57; ++i) start.set(i, 2);
         start.set(34, 12);
         start.set(39, 5);
         start.set(36, 13);
         start.set(61, 16);
         start.set(46, 31);
         start.set(43, 17);
         start.set(45, 18);
         start.set(60, 32);
         start.set(62, 20);
         start.set(124, 23);
         start.set(40, 33);
         start.set(41, 24);
         start.set(91, 25);
         start.set(93, 26);
         start.set(123, 27);
         start.set(125, 28);
         start.set(Buffer::EoF, -1);
         keywords.set(L"COMPILER", 6);
         keywords.set(L"IGNORECASE", 7);
         keywords.set(L"CHARACTERS", 8);
         keywords.set(L"TOKENS", 9);
         keywords.set(L"PRAGMAS", 10);
         keywords.set(L"COMMENTS", 11);
         keywords.set(L"FROM", 12);
         keywords.set(L"TO", 13);
         keywords.set(L"NESTED", 14);
         keywords.set(L"IGNORE", 15);
         keywords.set(L"PRODUCTIONS", 16);
         keywords.set(L"END", 19);
         keywords.set(L"ANY", 23);
         keywords.set(L"WEAK", 29);
         keywords.set(L"SYNC", 36);
         keywords.set(L"IF", 37);
         keywords.set(L"CONTEXT", 38);


         tvalLength = 128;
         tval = new wchar_t[tvalLength]; // text of current token

         // COCO_HEAP_BLOCK_SIZE byte heap + pointer to next heap block
         heap = malloc(COCO_HEAP_BLOCK_SIZE + sizeof(void*));
         firstHeap = heap;
         heapEnd = (void**) (((char*) heap) + COCO_HEAP_BLOCK_SIZE);
         *heapEnd = 0;
         heapTop = heap;
         if (sizeof(Token) > COCO_HEAP_BLOCK_SIZE) {
            wprintf(L"--- Too small COCO_HEAP_BLOCK_SIZE\n");
            exit(1);
         }

         pos = -1; line = 1; col = 0; charPos = -1;
         oldEols = 0;
         NextCh();
         if (ch == 0xEF) { // check optional byte order mark for UTF-8
            NextCh(); int ch1 = ch;
            NextCh(); int ch2 = ch;
            if (ch1 != 0xBB || ch2 != 0xBF) {
               wprintf(L"Illegal byte order mark at start of file");
               exit(1);
            }
            Buffer *oldBuf = buffer;
            buffer = new UTF8Buffer(buffer); col = 0; charPos = -1;
            delete oldBuf; oldBuf = NULL;
            NextCh();
         }


         pt = tokens = CreateToken(); // first token is a dummy
      }
#pragma warning(default: 4127)

      void Scanner::NextCh()
      {
         if (oldEols > 0) { ch = EOL; oldEols--; }
         else
         {
            pos = buffer->GetPos();
            // buffer reads unicode chars, if UTF8 has been detected
            ch = buffer->Read();
            col++;
            charPos++;
            // replace isolated '\r' by '\n' in order to make
            // eol handling uniform across Windows, Unix and Mac
            if (ch == L'\r' && buffer->Peek() != L'\n') ch = EOL;
            if (ch == EOL) { line++; col = 0; }
         }
      }

      void Scanner::AddCh()
      {
         if (tlen >= tvalLength)
         {
            tvalLength *= 2;
            wchar_t *newBuf = new wchar_t[tvalLength];
            memcpy(newBuf, tval, tlen*sizeof(wchar_t));
            delete [] tval;
            tval = newBuf;
         }
         if (ch != Buffer::EoF)
         {
            tval[tlen++] = (wchar_t)ch;
            NextCh();
         }
      }


      bool Scanner::Comment0()
      {
         int level = 1, pos0 = pos, line0 = line, col0 = col, charPos0 = charPos;
         NextCh();
         if (ch == L'/')
         {
            NextCh();
            for(;;)
            {
               if (ch == 10)
               {
                  level--;
                  if (level == 0) { oldEols = line - line0; NextCh(); return true; }
                  NextCh();
               }
               else if (ch == buffer->EoF) return false;
               else NextCh();
            }
         }
         else
         {
            buffer->SetPos(pos0);
            NextCh();
            line = line0;
            col = col0;
            charPos = charPos0;
         }
         return false;
      }

      bool Scanner::Comment1() 
      {
         int level = 1, pos0 = pos, line0 = line, col0 = col, charPos0 = charPos;
         NextCh();
         if (ch == L'*') {
            NextCh();
            for(;;) {
               if (ch == L'*') {
                  NextCh();
                  if (ch == L'/') {
                     level--;
                     if (level == 0) { oldEols = line - line0; NextCh(); return true; }
                     NextCh();
                  }
               } else if (ch == L'/') {
                  NextCh();
                  if (ch == L'*') {
                     level++; NextCh();
                  }
               } else if (ch == buffer->EoF) return false;
               else NextCh();
            }
         } else {
            buffer->SetPos(pos0); NextCh(); line = line0; col = col0; charPos = charPos0;
         }
         return false;
      }


      void Scanner::CreateHeapBlock() {
         void* newHeap;
         char* cur = (char*) firstHeap;

         while(((char*) tokens < cur) || ((char*) tokens > (cur + COCO_HEAP_BLOCK_SIZE))) {
            cur = *((char**) (cur + COCO_HEAP_BLOCK_SIZE));
            free(firstHeap);
            firstHeap = cur;
         }

         // COCO_HEAP_BLOCK_SIZE byte heap + pointer to next heap block
         newHeap = malloc(COCO_HEAP_BLOCK_SIZE + sizeof(void*));
         *heapEnd = newHeap;
         heapEnd = (void**) (((char*) newHeap) + COCO_HEAP_BLOCK_SIZE);
         *heapEnd = 0;
         heap = newHeap;
         heapTop = heap;
      }

      Token* Scanner::CreateToken() {
         Token *t;
         if (((char*) heapTop + (int) sizeof(Token)) >= (char*) heapEnd) {
            CreateHeapBlock();
         }
         t = (Token*) heapTop;
         heapTop = (void*) ((char*) heapTop + sizeof(Token));
         t->val = NULL;
         t->next = NULL;
         return t;
      }

      void Scanner::AppendVal(Token *t) {
         int reqMem = (tlen + 1) * sizeof(wchar_t);
         if (((char*) heapTop + reqMem) >= (char*) heapEnd) {
            if (reqMem > COCO_HEAP_BLOCK_SIZE) {
               wprintf(L"--- Too long token value\n");
               exit(1);
            }
            CreateHeapBlock();
         }
         t->val = (wchar_t*) heapTop;
         heapTop = (void*) ((char*) heapTop + reqMem);

         wcsncpy(t->val, tval, tlen);
         t->val[tlen] = L'\0';
      }

      Token* Scanner::NextToken()
      {
         while (ch == ' ' ||
            (ch >= 9 && ch <= 10) || ch == 13
            ) NextCh();
         if ((ch == L'/' && Comment0()) || (ch == L'/' && Comment1())) return NextToken();
         int recKind = noSym;
         int recEnd = pos;
         t = CreateToken();
         t->pos = pos;
         t->col = col;
         t->line = line;
         t->charPos = charPos;
         int state = start.state(ch);
         tlen = 0; AddCh();

         switch (state)
         {
         case -1: { t->kind = eofSym; break; } // NextCh already done
         case 0:
            {
case_0:
            if (recKind != noSym) {
               tlen = recEnd - t->pos;
               SetScannerBehindT();
            }
            t->kind = recKind; break;
                 } // NextCh already done
         case 1:
case_1:
            recEnd = pos; recKind = 1;
            if ((ch >= L'0' && ch <= L'9') || (ch >= L'A' && ch <= L'Z') || ch == L'_' || (ch >= L'a' && ch <= L'z')) {AddCh(); goto case_1;}
            else {t->kind = 1; wchar_t *literal = coco_string_create(tval, 0, tlen); t->kind = keywords.get(literal, t->kind); coco_string_delete(literal); break;}
         case 2:
case_2:
            recEnd = pos; recKind = 2;
            if ((ch >= L'0' && ch <= L'9')) {AddCh(); goto case_2;}
            else {t->kind = 2; break;}
         case 3:
case_3:
            {t->kind = 3; break;}
         case 4:
case_4:
            {t->kind = 4; break;}
         case 5:
            if (ch <= 9 || (ch >= 11 && ch <= 12) || (ch >= 14 && ch <= L'&') || (ch >= L'(' && ch <= L'[') || (ch >= L']' && ch <= 65535)) {AddCh(); goto case_6;}
            else if (ch == 92) {AddCh(); goto case_7;}
            else {goto case_0;}
         case 6:
case_6:
            if (ch == 39) {AddCh(); goto case_9;}
            else {goto case_0;}
         case 7:
case_7:
            if ((ch >= L' ' && ch <= L'~')) {AddCh(); goto case_8;}
            else {goto case_0;}
         case 8:
case_8:
            if ((ch >= L'0' && ch <= L'9') || (ch >= L'a' && ch <= L'f')) {AddCh(); goto case_8;}
            else if (ch == 39) {AddCh(); goto case_9;}
            else {goto case_0;}
         case 9:
case_9:
            {t->kind = 5; break;}
         case 10:
case_10:
            recEnd = pos; recKind = 42;
            if ((ch >= L'0' && ch <= L'9') || (ch >= L'A' && ch <= L'Z') || ch == L'_' || (ch >= L'a' && ch <= L'z')) {AddCh(); goto case_10;}
            else {t->kind = 42; break;}
         case 11:
case_11:
            recEnd = pos; recKind = 43;
            if ((ch >= L'-' && ch <= L'.') || (ch >= L'0' && ch <= L':') || (ch >= L'A' && ch <= L'Z') || ch == L'_' || (ch >= L'a' && ch <= L'z')) {AddCh(); goto case_11;}
            else {t->kind = 43; break;}
         case 12:
case_12:
            if (ch <= 9 || (ch >= 11 && ch <= 12) || (ch >= 14 && ch <= L'!') || (ch >= L'#' && ch <= L'[') || (ch >= L']' && ch <= 65535)) {AddCh(); goto case_12;}
            else if (ch == 10 || ch == 13) {AddCh(); goto case_4;}
            else if (ch == L'"') {AddCh(); goto case_3;}
            else if (ch == 92) {AddCh(); goto case_14;}
            else {goto case_0;}
         case 13:
            recEnd = pos; recKind = 42;
            if ((ch >= L'0' && ch <= L'9')) {AddCh(); goto case_10;}
            else if ((ch >= L'A' && ch <= L'Z') || ch == L'_' || (ch >= L'a' && ch <= L'z')) {AddCh(); goto case_15;}
            else {t->kind = 42; break;}
         case 14:
case_14:
            if ((ch >= L' ' && ch <= L'~')) {AddCh(); goto case_12;}
            else {goto case_0;}
         case 15:
case_15:
            recEnd = pos; recKind = 42;
            if ((ch >= L'0' && ch <= L'9')) {AddCh(); goto case_10;}
            else if ((ch >= L'A' && ch <= L'Z') || ch == L'_' || (ch >= L'a' && ch <= L'z')) {AddCh(); goto case_15;}
            else if (ch == L'=') {AddCh(); goto case_11;}
            else {t->kind = 42; break;}
         case 16:
            {t->kind = 17; break;}
         case 17:
            {t->kind = 20; break;}
         case 18:
            {t->kind = 21; break;}
         case 19:
case_19:
            {t->kind = 22; break;}
         case 20:
            {t->kind = 25; break;}
         case 21:
case_21:
            {t->kind = 26; break;}
         case 22:
case_22:
            {t->kind = 27; break;}
         case 23:
            {t->kind = 28; break;}
         case 24:
            {t->kind = 31; break;}
         case 25:
            {t->kind = 32; break;}
         case 26:
            {t->kind = 33; break;}
         case 27:
            {t->kind = 34; break;}
         case 28:
            {t->kind = 35; break;}
         case 29:
case_29:
            {t->kind = 39; break;}
         case 30:
case_30:
            {t->kind = 40; break;}
         case 31:
            recEnd = pos; recKind = 18;
            if (ch == L'.') {AddCh(); goto case_19;}
            else if (ch == L'>') {AddCh(); goto case_22;}
            else if (ch == L')') {AddCh(); goto case_30;}
            else {t->kind = 18; break;}
         case 32:
            recEnd = pos; recKind = 24;
            if (ch == L'.') {AddCh(); goto case_21;}
            else {t->kind = 24; break;}
         case 33:
            recEnd = pos; recKind = 30;
            if (ch == L'.') {AddCh(); goto case_29;}
            else {t->kind = 30; break;}

         }
         AppendVal(t);
         return t;
      }

      void Scanner::SetScannerBehindT()
      {
         buffer->SetPos(t->pos);
         NextCh();
         line = t->line;
         col = t->col;
         charPos = t->charPos;
         for (int i = 0; i < tlen; i++) NextCh();
      }

      // get the next token (possibly a token already seen during peeking)
      Token* Scanner::Scan()
      {
         if (tokens->next == NULL)
         {
            return pt = tokens = NextToken();
         }
         else
         {
            pt = tokens = tokens->next;
            return tokens;
         }
      }

      // peek for the next token, ignore pragmas
      Token* Scanner::Peek()
      {
         do
         {
            if (pt->next == NULL)
            {
               pt->next = NextToken();
            }
            pt = pt->next;
         } while (pt->kind > maxT); // skip pragmas

         return pt;
      }

      // make sure that peeking starts at the current scan position
      void Scanner::ResetPeek()
      {
         pt = tokens;
      }
   }
}