#include "CsScanner.h"
#include <stdexcept>

namespace Coco
{
   namespace Cs
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

      Scanner::~Scanner()
      {
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
         for (int i = 65; i <= 90; ++i) start.set(i, 1);
         for (int i = 95; i <= 95; ++i) start.set(i, 1);
         for (int i = 97; i <= 122; ++i) start.set(i, 1);
         for (int i = 170; i <= 170; ++i) start.set(i, 1);
         for (int i = 181; i <= 181; ++i) start.set(i, 1);
         for (int i = 186; i <= 186; ++i) start.set(i, 1);
         for (int i = 192; i <= 214; ++i) start.set(i, 1);
         for (int i = 216; i <= 246; ++i) start.set(i, 1);
         for (int i = 248; i <= 255; ++i) start.set(i, 1);
         for (int i = 49; i <= 57; ++i) start.set(i, 158);
         start.set(92, 15);
         start.set(64, 159);
         start.set(48, 160);
         start.set(46, 161);
         start.set(39, 44);
         start.set(34, 61);
         start.set(38, 162);
         start.set(61, 163);
         start.set(58, 164);
         start.set(44, 80);
         start.set(45, 195);
         start.set(47, 196);
         start.set(62, 165);
         start.set(43, 166);
         start.set(123, 87);
         start.set(91, 88);
         start.set(40, 89);
         start.set(60, 167);
         start.set(37, 197);
         start.set(33, 168);
         start.set(63, 169);
         start.set(124, 198);
         start.set(125, 97);
         start.set(93, 98);
         start.set(41, 99);
         start.set(59, 100);
         start.set(126, 101);
         start.set(42, 170);
         start.set(94, 199);
         start.set(35, 171);
         start.set(Buffer::EoF, -1);

         keywords.set(L"abstract", 6);
         keywords.set(L"as", 7);
         keywords.set(L"base", 8);
         keywords.set(L"bool", 9);
         keywords.set(L"break", 10);
         keywords.set(L"byte", 11);
         keywords.set(L"case", 12);
         keywords.set(L"catch", 13);
         keywords.set(L"char", 14);
         keywords.set(L"checked", 15);
         keywords.set(L"class", 16);
         keywords.set(L"const", 17);
         keywords.set(L"continue", 18);
         keywords.set(L"decimal", 19);
         keywords.set(L"default", 20);
         keywords.set(L"delegate", 21);
         keywords.set(L"do", 22);
         keywords.set(L"double", 23);
         keywords.set(L"else", 24);
         keywords.set(L"enum", 25);
         keywords.set(L"event", 26);
         keywords.set(L"explicit", 27);
         keywords.set(L"extern", 28);
         keywords.set(L"false", 29);
         keywords.set(L"finally", 30);
         keywords.set(L"fixed", 31);
         keywords.set(L"float", 32);
         keywords.set(L"for", 33);
         keywords.set(L"foreach", 34);
         keywords.set(L"goto", 35);
         keywords.set(L"if", 36);
         keywords.set(L"implicit", 37);
         keywords.set(L"in", 38);
         keywords.set(L"int", 39);
         keywords.set(L"interface", 40);
         keywords.set(L"internal", 41);
         keywords.set(L"is", 42);
         keywords.set(L"lock", 43);
         keywords.set(L"long", 44);
         keywords.set(L"namespace", 45);
         keywords.set(L"new", 46);
         keywords.set(L"null", 47);
         keywords.set(L"object", 48);
         keywords.set(L"operator", 49);
         keywords.set(L"out", 50);
         keywords.set(L"override", 51);
         keywords.set(L"params", 52);
         keywords.set(L"private", 53);
         keywords.set(L"protected", 54);
         keywords.set(L"public", 55);
         keywords.set(L"readonly", 56);
         keywords.set(L"ref", 57);
         keywords.set(L"return", 58);
         keywords.set(L"sbyte", 59);
         keywords.set(L"sealed", 60);
         keywords.set(L"short", 61);
         keywords.set(L"sizeof", 62);
         keywords.set(L"stackalloc", 63);
         keywords.set(L"static", 64);
         keywords.set(L"string", 65);
         keywords.set(L"struct", 66);
         keywords.set(L"switch", 67);
         keywords.set(L"this", 68);
         keywords.set(L"throw", 69);
         keywords.set(L"true", 70);
         keywords.set(L"try", 71);
         keywords.set(L"typeof", 72);
         keywords.set(L"uint", 73);
         keywords.set(L"ulong", 74);
         keywords.set(L"unchecked", 75);
         keywords.set(L"unsafe", 76);
         keywords.set(L"ushort", 77);
         keywords.set(L"using", 78);
         keywords.set(L"virtual", 79);
         keywords.set(L"void", 80);
         keywords.set(L"volatile", 81);
         keywords.set(L"while", 82);
         keywords.set(L"from", 123);
         keywords.set(L"where", 124);
         keywords.set(L"join", 125);
         keywords.set(L"on", 126);
         keywords.set(L"equals", 127);
         keywords.set(L"into", 128);
         keywords.set(L"let", 129);
         keywords.set(L"orderby", 130);
         keywords.set(L"ascending", 131);
         keywords.set(L"descending", 132);
         keywords.set(L"select", 133);
         keywords.set(L"group", 134);
         keywords.set(L"by", 135);

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
            ch >= 9 && ch <= 10 || ch == 13
            ) NextCh();
         if (ch == '/' && Comment0() ||ch == '/' && Comment1()) return NextToken();
         int apx = 0;
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
            if (ch >= '0' && ch <= '9' || ch >= 'A' && ch <= 'Z' || ch == '_' || ch >= 'a' && ch <= 'z' || ch == 128 || ch >= 160 && ch <= 179 || ch == 181 || ch == 186 || ch >= 192 && ch <= 214 || ch >= 216 && ch <= 246 || ch >= 248 && ch <= 255) {AddCh(); goto case_1;}
            else if (ch == 92) {AddCh(); goto case_2;}
            //else {t->kind = 1; t->val = new String(tval, 0, tlen); CheckLiteral(); return t;}
            else {t->kind = 1; wchar_t *literal = coco_string_create(tval, 0, tlen); t->kind = keywords.get(literal, t->kind); coco_string_delete(literal); break;}
         case 2:
case_2:
            if (ch == 'u') {AddCh(); goto case_3;}
            else if (ch == 'U') {AddCh(); goto case_7;}
            else {goto case_0;}
         case 3:
case_3:
            if (ch >= '0' && ch <= '9' || ch >= 'A' && ch <= 'F' || ch >= 'a' && ch <= 'f') {AddCh(); goto case_4;}
            else {goto case_0;}
         case 4:
case_4:
            if (ch >= '0' && ch <= '9' || ch >= 'A' && ch <= 'F' || ch >= 'a' && ch <= 'f') {AddCh(); goto case_5;}
            else {goto case_0;}
         case 5:
case_5:
            if (ch >= '0' && ch <= '9' || ch >= 'A' && ch <= 'F' || ch >= 'a' && ch <= 'f') {AddCh(); goto case_6;}
            else {goto case_0;}
         case 6:
case_6:
            if (ch >= '0' && ch <= '9' || ch >= 'A' && ch <= 'F' || ch >= 'a' && ch <= 'f') {AddCh(); goto case_1;}
            else {goto case_0;}
         case 7:
case_7:
            if (ch >= '0' && ch <= '9' || ch >= 'A' && ch <= 'F' || ch >= 'a' && ch <= 'f') {AddCh(); goto case_8;}
            else {goto case_0;}
         case 8:
case_8:
            if (ch >= '0' && ch <= '9' || ch >= 'A' && ch <= 'F' || ch >= 'a' && ch <= 'f') {AddCh(); goto case_9;}
            else {goto case_0;}
         case 9:
case_9:
            if (ch >= '0' && ch <= '9' || ch >= 'A' && ch <= 'F' || ch >= 'a' && ch <= 'f') {AddCh(); goto case_10;}
            else {goto case_0;}
         case 10:
case_10:
            if (ch >= '0' && ch <= '9' || ch >= 'A' && ch <= 'F' || ch >= 'a' && ch <= 'f') {AddCh(); goto case_11;}
            else {goto case_0;}
         case 11:
case_11:
            if (ch >= '0' && ch <= '9' || ch >= 'A' && ch <= 'F' || ch >= 'a' && ch <= 'f') {AddCh(); goto case_12;}
            else {goto case_0;}
         case 12:
case_12:
            if (ch >= '0' && ch <= '9' || ch >= 'A' && ch <= 'F' || ch >= 'a' && ch <= 'f') {AddCh(); goto case_13;}
            else {goto case_0;}
         case 13:
case_13:
            if (ch >= '0' && ch <= '9' || ch >= 'A' && ch <= 'F' || ch >= 'a' && ch <= 'f') {AddCh(); goto case_14;}
            else {goto case_0;}
         case 14:
case_14:
            if (ch >= '0' && ch <= '9' || ch >= 'A' && ch <= 'F' || ch >= 'a' && ch <= 'f') {AddCh(); goto case_1;}
            else {goto case_0;}
         case 15:
case_15:
            if (ch == 'u') {AddCh(); goto case_16;}
            else if (ch == 'U') {AddCh(); goto case_20;}
            else {goto case_0;}
         case 16:
case_16:
            if (ch >= '0' && ch <= '9' || ch >= 'A' && ch <= 'F' || ch >= 'a' && ch <= 'f') {AddCh(); goto case_17;}
            else {goto case_0;}
         case 17:
case_17:
            if (ch >= '0' && ch <= '9' || ch >= 'A' && ch <= 'F' || ch >= 'a' && ch <= 'f') {AddCh(); goto case_18;}
            else {goto case_0;}
         case 18:
case_18:
            if (ch >= '0' && ch <= '9' || ch >= 'A' && ch <= 'F' || ch >= 'a' && ch <= 'f') {AddCh(); goto case_19;}
            else {goto case_0;}
         case 19:
case_19:
            if (ch >= '0' && ch <= '9' || ch >= 'A' && ch <= 'F' || ch >= 'a' && ch <= 'f') {AddCh(); goto case_1;}
            else {goto case_0;}
         case 20:
case_20:
            if (ch >= '0' && ch <= '9' || ch >= 'A' && ch <= 'F' || ch >= 'a' && ch <= 'f') {AddCh(); goto case_21;}
            else {goto case_0;}
         case 21:
case_21:
            if (ch >= '0' && ch <= '9' || ch >= 'A' && ch <= 'F' || ch >= 'a' && ch <= 'f') {AddCh(); goto case_22;}
            else {goto case_0;}
         case 22:
case_22:
            if (ch >= '0' && ch <= '9' || ch >= 'A' && ch <= 'F' || ch >= 'a' && ch <= 'f') {AddCh(); goto case_23;}
            else {goto case_0;}
         case 23:
case_23:
            if (ch >= '0' && ch <= '9' || ch >= 'A' && ch <= 'F' || ch >= 'a' && ch <= 'f') {AddCh(); goto case_24;}
            else {goto case_0;}
         case 24:
case_24:
            if (ch >= '0' && ch <= '9' || ch >= 'A' && ch <= 'F' || ch >= 'a' && ch <= 'f') {AddCh(); goto case_25;}
            else {goto case_0;}
         case 25:
case_25:
            if (ch >= '0' && ch <= '9' || ch >= 'A' && ch <= 'F' || ch >= 'a' && ch <= 'f') {AddCh(); goto case_26;}
            else {goto case_0;}
         case 26:
case_26:
            if (ch >= '0' && ch <= '9' || ch >= 'A' && ch <= 'F' || ch >= 'a' && ch <= 'f') {AddCh(); goto case_27;}
            else {goto case_0;}
         case 27:
case_27:
            if (ch >= '0' && ch <= '9' || ch >= 'A' && ch <= 'F' || ch >= 'a' && ch <= 'f') {AddCh(); goto case_1;}
            else {goto case_0;}
         case 28:
            {
case_28:
               tlen -= apx;
               SetScannerBehindT();
               t->kind = 2; break;}
         case 29:
case_29:
            if (ch >= '0' && ch <= '9' || ch >= 'A' && ch <= 'F' || ch >= 'a' && ch <= 'f') {AddCh(); goto case_30;}
            else {goto case_0;}
         case 30:
case_30:
            recEnd = pos; recKind = 2;
            if (ch >= '0' && ch <= '9' || ch >= 'A' && ch <= 'F' || ch >= 'a' && ch <= 'f') {AddCh(); goto case_30;}
            else if (ch == 'U') {AddCh(); goto case_176;}
            else if (ch == 'u') {AddCh(); goto case_177;}
            else if (ch == 'L') {AddCh(); goto case_178;}
            else if (ch == 'l') {AddCh(); goto case_179;}
            else {t->kind = 2; break;}
         case 31:
            {
case_31:
               t->kind = 2; break;}
         case 32:
case_32:
            recEnd = pos; recKind = 3;
            if (ch >= '0' && ch <= '9') {AddCh(); goto case_32;}
            else if (ch == 'D' || ch == 'F' || ch == 'M' || ch == 'd' || ch == 'f' || ch == 'm') {AddCh(); goto case_43;}
            else if (ch == 'E' || ch == 'e') {AddCh(); goto case_33;}
            else {t->kind = 3; break;}
         case 33:
case_33:
            if (ch >= '0' && ch <= '9') {AddCh(); goto case_35;}
            else if (ch == '+' || ch == '-') {AddCh(); goto case_34;}
            else {goto case_0;}
         case 34:
case_34:
            if (ch >= '0' && ch <= '9') {AddCh(); goto case_35;}
            else {goto case_0;}
         case 35:
case_35:
            recEnd = pos; recKind = 3;
            if (ch >= '0' && ch <= '9') {AddCh(); goto case_35;}
            else if (ch == 'D' || ch == 'F' || ch == 'M' || ch == 'd' || ch == 'f' || ch == 'm') {AddCh(); goto case_43;}
            else {t->kind = 3; break;}
         case 36:
case_36:
            recEnd = pos; recKind = 3;
            if (ch >= '0' && ch <= '9') {AddCh(); goto case_36;}
            else if (ch == 'D' || ch == 'F' || ch == 'M' || ch == 'd' || ch == 'f' || ch == 'm') {AddCh(); goto case_43;}
            else if (ch == 'E' || ch == 'e') {AddCh(); goto case_37;}
            else {t->kind = 3; break;}
         case 37:
case_37:
            if (ch >= '0' && ch <= '9') {AddCh(); goto case_39;}
            else if (ch == '+' || ch == '-') {AddCh(); goto case_38;}
            else {goto case_0;}
         case 38:
case_38:
            if (ch >= '0' && ch <= '9') {AddCh(); goto case_39;}
            else {goto case_0;}
         case 39:
case_39:
            recEnd = pos; recKind = 3;
            if (ch >= '0' && ch <= '9') {AddCh(); goto case_39;}
            else if (ch == 'D' || ch == 'F' || ch == 'M' || ch == 'd' || ch == 'f' || ch == 'm') {AddCh(); goto case_43;}
            else {t->kind = 3; break;}
         case 40:
case_40:
            if (ch >= '0' && ch <= '9') {AddCh(); goto case_42;}
            else if (ch == '+' || ch == '-') {AddCh(); goto case_41;}
            else {goto case_0;}
         case 41:
case_41:
            if (ch >= '0' && ch <= '9') {AddCh(); goto case_42;}
            else {goto case_0;}
         case 42:
case_42:
            recEnd = pos; recKind = 3;
            if (ch >= '0' && ch <= '9') {AddCh(); goto case_42;}
            else if (ch == 'D' || ch == 'F' || ch == 'M' || ch == 'd' || ch == 'f' || ch == 'm') {AddCh(); goto case_43;}
            else {t->kind = 3; break;}
         case 43:
case_43:
            {t->kind = 3; break;}
         case 44:
            //            case_44:
            if (ch <= 9 || ch >= 11 && ch <= 12 || ch >= 14 && ch <= '&' || ch >= '(' && ch <= '[' || ch >= ']' && ch <= 65535) {AddCh(); goto case_45;}
            else if (ch == 92) {AddCh(); goto case_180;}
            else {goto case_0;}
         case 45:
case_45:
            if (ch == 39) {AddCh(); goto case_60;}
            else {goto case_0;}
         case 46:
case_46:
            if (ch >= '0' && ch <= '9' || ch >= 'A' && ch <= 'F' || ch >= 'a' && ch <= 'f') {AddCh(); goto case_47;}
            else {goto case_0;}
         case 47:
case_47:
            if (ch >= '0' && ch <= '9' || ch >= 'A' && ch <= 'F' || ch >= 'a' && ch <= 'f') {AddCh(); goto case_181;}
            else if (ch == 39) {AddCh(); goto case_60;}
            else {goto case_0;}
         case 48:
case_48:
            if (ch >= '0' && ch <= '9' || ch >= 'A' && ch <= 'F' || ch >= 'a' && ch <= 'f') {AddCh(); goto case_49;}
            else {goto case_0;}
         case 49:
case_49:
            if (ch >= '0' && ch <= '9' || ch >= 'A' && ch <= 'F' || ch >= 'a' && ch <= 'f') {AddCh(); goto case_50;}
            else {goto case_0;}
         case 50:
case_50:
            if (ch >= '0' && ch <= '9' || ch >= 'A' && ch <= 'F' || ch >= 'a' && ch <= 'f') {AddCh(); goto case_51;}
            else {goto case_0;}
         case 51:
case_51:
            if (ch >= '0' && ch <= '9' || ch >= 'A' && ch <= 'F' || ch >= 'a' && ch <= 'f') {AddCh(); goto case_45;}
            else {goto case_0;}
         case 52:
case_52:
            if (ch >= '0' && ch <= '9' || ch >= 'A' && ch <= 'F' || ch >= 'a' && ch <= 'f') {AddCh(); goto case_53;}
            else {goto case_0;}
         case 53:
case_53:
            if (ch >= '0' && ch <= '9' || ch >= 'A' && ch <= 'F' || ch >= 'a' && ch <= 'f') {AddCh(); goto case_54;}
            else {goto case_0;}
         case 54:
case_54:
            if (ch >= '0' && ch <= '9' || ch >= 'A' && ch <= 'F' || ch >= 'a' && ch <= 'f') {AddCh(); goto case_55;}
            else {goto case_0;}
         case 55:
case_55:
            if (ch >= '0' && ch <= '9' || ch >= 'A' && ch <= 'F' || ch >= 'a' && ch <= 'f') {AddCh(); goto case_56;}
            else {goto case_0;}
         case 56:
case_56:
            if (ch >= '0' && ch <= '9' || ch >= 'A' && ch <= 'F' || ch >= 'a' && ch <= 'f') {AddCh(); goto case_57;}
            else {goto case_0;}
         case 57:
case_57:
            if (ch >= '0' && ch <= '9' || ch >= 'A' && ch <= 'F' || ch >= 'a' && ch <= 'f') {AddCh(); goto case_58;}
            else {goto case_0;}
         case 58:
case_58:
            if (ch >= '0' && ch <= '9' || ch >= 'A' && ch <= 'F' || ch >= 'a' && ch <= 'f') {AddCh(); goto case_59;}
            else {goto case_0;}
         case 59:
case_59:
            if (ch >= '0' && ch <= '9' || ch >= 'A' && ch <= 'F' || ch >= 'a' && ch <= 'f') {AddCh(); goto case_45;}
            else {goto case_0;}
         case 60:
            {
case_60:
               t->kind = 4; break;}
         case 61:
case_61:
            if (ch <= 9 || ch >= 11 && ch <= 12 || ch >= 14 && ch <= '!' || ch >= '#' && ch <= '[' || ch >= ']' && ch <= 65535) {AddCh(); goto case_61;}
            else if (ch == '"') {AddCh(); goto case_77;}
            else if (ch == 92) {AddCh(); goto case_183;}
            else {goto case_0;}
         case 62:
case_62:
            if (ch >= '0' && ch <= '9' || ch >= 'A' && ch <= 'F' || ch >= 'a' && ch <= 'f') {AddCh(); goto case_63;}
            else {goto case_0;}
         case 63:
case_63:
            if (ch <= 9 || ch >= 11 && ch <= 12 || ch >= 14 && ch <= '!' || ch >= '#' && ch <= '/' || ch >= ':' && ch <= '@' || ch >= 'G' && ch <= '[' || ch >= ']' && ch <= '`' || ch >= 'g' && ch <= 65535) {AddCh(); goto case_61;}
            else if (ch >= '0' && ch <= '9' || ch >= 'A' && ch <= 'F' || ch >= 'a' && ch <= 'f') {AddCh(); goto case_184;}
            else if (ch == '"') {AddCh(); goto case_77;}
            else if (ch == 92) {AddCh(); goto case_183;}
            else {goto case_0;}
         case 64:
case_64:
            if (ch >= '0' && ch <= '9' || ch >= 'A' && ch <= 'F' || ch >= 'a' && ch <= 'f') {AddCh(); goto case_65;}
            else {goto case_0;}
         case 65:
case_65:
            if (ch >= '0' && ch <= '9' || ch >= 'A' && ch <= 'F' || ch >= 'a' && ch <= 'f') {AddCh(); goto case_66;}
            else {goto case_0;}
         case 66:
case_66:
            if (ch >= '0' && ch <= '9' || ch >= 'A' && ch <= 'F' || ch >= 'a' && ch <= 'f') {AddCh(); goto case_67;}
            else {goto case_0;}
         case 67:
case_67:
            if (ch >= '0' && ch <= '9' || ch >= 'A' && ch <= 'F' || ch >= 'a' && ch <= 'f') {AddCh(); goto case_61;}
            else {goto case_0;}
         case 68:
case_68:
            if (ch >= '0' && ch <= '9' || ch >= 'A' && ch <= 'F' || ch >= 'a' && ch <= 'f') {AddCh(); goto case_69;}
            else {goto case_0;}
         case 69:
case_69:
            if (ch >= '0' && ch <= '9' || ch >= 'A' && ch <= 'F' || ch >= 'a' && ch <= 'f') {AddCh(); goto case_70;}
            else {goto case_0;}
         case 70:
case_70:
            if (ch >= '0' && ch <= '9' || ch >= 'A' && ch <= 'F' || ch >= 'a' && ch <= 'f') {AddCh(); goto case_71;}
            else {goto case_0;}
         case 71:
case_71:
            if (ch >= '0' && ch <= '9' || ch >= 'A' && ch <= 'F' || ch >= 'a' && ch <= 'f') {AddCh(); goto case_72;}
            else {goto case_0;}
         case 72:
case_72:
            if (ch >= '0' && ch <= '9' || ch >= 'A' && ch <= 'F' || ch >= 'a' && ch <= 'f') {AddCh(); goto case_73;}
            else {goto case_0;}
         case 73:
case_73:
            if (ch >= '0' && ch <= '9' || ch >= 'A' && ch <= 'F' || ch >= 'a' && ch <= 'f') {AddCh(); goto case_74;}
            else {goto case_0;}
         case 74:
case_74:
            if (ch >= '0' && ch <= '9' || ch >= 'A' && ch <= 'F' || ch >= 'a' && ch <= 'f') {AddCh(); goto case_75;}
            else {goto case_0;}
         case 75:
case_75:
            if (ch >= '0' && ch <= '9' || ch >= 'A' && ch <= 'F' || ch >= 'a' && ch <= 'f') {AddCh(); goto case_61;}
            else {goto case_0;}
         case 76:
case_76:
            if (ch <= '!' || ch >= '#' && ch <= 65535) {AddCh(); goto case_76;}
            else if (ch == '"') {AddCh(); goto case_186;}
            else {goto case_0;}
         case 77:
            {
case_77:
               t->kind = 5; break;}
         case 78:
            {
case_78:
               t->kind = 84; break;}
         case 79:
            {
case_79:
               t->kind = 85; break;}
         case 80:
            {
               //               case_80:
               t->kind = 88; break;}
         case 81:
            {case_81:
            t->kind = 89; break;}
         case 82:
            {
case_82:
               t->kind = 90; break;}
         case 83:
            {
case_83:
               t->kind = 92; break;}
         case 84:
            {
case_84:
               t->kind = 93; break;}
         case 85:
            {
case_85:
               t->kind = 95; break;}
         case 86:
            {
case_86:
               t->kind = 96; break;}
         case 87:
            {t->kind = 97; break;}
         case 88:
            {t->kind = 98; break;}
         case 89:
            {t->kind = 99; break;}
         case 90:
            {
case_90:
               t->kind = 100; break;}
         case 91:
            {
case_91:
               t->kind = 104; break;}
         case 92:
            {
case_92:
               t->kind = 105; break;}
         case 93:
            {
case_93:
               t->kind = 106; break;}
         case 94:
            {
case_94:
               t->kind = 108; break;}
         case 95:
            {
case_95:
               t->kind = 109; break;}
         case 96:
            {
case_96:
               t->kind = 111; break;}
         case 97:
            {t->kind = 113; break;}
         case 98:
            {t->kind = 114; break;}
         case 99:
            {t->kind = 115; break;}
         case 100:
            {t->kind = 116; break;}
         case 101:
            {t->kind = 117; break;}
         case 102:
            {
case_102:
               t->kind = 119; break;}
         case 103:
            {
case_103:
               t->kind = 120; break;}
         case 104:
            {
case_104:
               t->kind = 121; break;}
         case 105:
            {
case_105:
               t->kind = 122; break;}
         case 106:
case_106:
            if (ch == 'e') {AddCh(); goto case_107;}
            else {goto case_0;}
         case 107:
case_107:
            if (ch == 'f') {AddCh(); goto case_108;}
            else {goto case_0;}
         case 108:
case_108:
            if (ch == 'i') {AddCh(); goto case_109;}
            else {goto case_0;}
         case 109:
case_109:
            if (ch == 'n') {AddCh(); goto case_110;}
            else {goto case_0;}
         case 110:
case_110:
            if (ch == 'e') {AddCh(); goto case_111;}
            else {goto case_0;}
         case 111:
case_111:
            recEnd = pos; recKind = 143;
            if (ch <= 9 || ch >= 11 && ch <= 12 || ch >= 14 && ch <= 65535) {AddCh(); goto case_111;}
            else {t->kind = 143; break;}
         case 112:
case_112:
            if (ch == 'n') {AddCh(); goto case_113;}
            else {goto case_0;}
         case 113:
case_113:
            if (ch == 'd') {AddCh(); goto case_114;}
            else {goto case_0;}
         case 114:
case_114:
            if (ch == 'e') {AddCh(); goto case_115;}
            else {goto case_0;}
         case 115:
case_115:
            if (ch == 'f') {AddCh(); goto case_116;}
            else {goto case_0;}
         case 116:
case_116:
            recEnd = pos; recKind = 144;
            if (ch <= 9 || ch >= 11 && ch <= 12 || ch >= 14 && ch <= 65535) {AddCh(); goto case_116;}
            else {t->kind = 144; break;}
         case 117:
case_117:
            if (ch == 'f') {AddCh(); goto case_118;}
            else {goto case_0;}
         case 118:
case_118:
            recEnd = pos; recKind = 145;
            if (ch <= 9 || ch >= 11 && ch <= 12 || ch >= 14 && ch <= 65535) {AddCh(); goto case_118;}
            else {t->kind = 145; break;}
         case 119:
case_119:
            if (ch == 'f') {AddCh(); goto case_120;}
            else {goto case_0;}
         case 120:
case_120:
            recEnd = pos; recKind = 146;
            if (ch <= 9 || ch >= 11 && ch <= 12 || ch >= 14 && ch <= 65535) {AddCh(); goto case_120;}
            else {t->kind = 146; break;}
         case 121:
case_121:
            if (ch == 'e') {AddCh(); goto case_122;}
            else {goto case_0;}
         case 122:
case_122:
            recEnd = pos; recKind = 147;
            if (ch <= 9 || ch >= 11 && ch <= 12 || ch >= 14 && ch <= 65535) {AddCh(); goto case_122;}
            else {t->kind = 147; break;}
         case 123:
case_123:
            if (ch == 'f') {AddCh(); goto case_124;}
            else {goto case_0;}
         case 124:
case_124:
            recEnd = pos; recKind = 148;
            if (ch <= 9 || ch >= 11 && ch <= 12 || ch >= 14 && ch <= 65535) {AddCh(); goto case_124;}
            else {t->kind = 148; break;}
         case 125:
case_125:
            if (ch == 'i') {AddCh(); goto case_126;}
            else {goto case_0;}
         case 126:
case_126:
            if (ch == 'n') {AddCh(); goto case_127;}
            else {goto case_0;}
         case 127:
case_127:
            if (ch == 'e') {AddCh(); goto case_128;}
            else {goto case_0;}
         case 128:
case_128:
            recEnd = pos; recKind = 149;
            if (ch <= 9 || ch >= 11 && ch <= 12 || ch >= 14 && ch <= 65535) {AddCh(); goto case_128;}
            else {t->kind = 149; break;}
         case 129:
case_129:
            if (ch == 'r') {AddCh(); goto case_130;}
            else {goto case_0;}
         case 130:
case_130:
            if (ch == 'o') {AddCh(); goto case_131;}
            else {goto case_0;}
         case 131:
case_131:
            if (ch == 'r') {AddCh(); goto case_132;}
            else {goto case_0;}
         case 132:
case_132:
            recEnd = pos; recKind = 150;
            if (ch <= 9 || ch >= 11 && ch <= 12 || ch >= 14 && ch <= 65535) {AddCh(); goto case_132;}
            else {t->kind = 150; break;}
         case 133:
case_133:
            if (ch == 'a') {AddCh(); goto case_134;}
            else {goto case_0;}
         case 134:
case_134:
            if (ch == 'r') {AddCh(); goto case_135;}
            else {goto case_0;}
         case 135:
case_135:
            if (ch == 'n') {AddCh(); goto case_136;}
            else {goto case_0;}
         case 136:
case_136:
            if (ch == 'i') {AddCh(); goto case_137;}
            else {goto case_0;}
         case 137:
case_137:
            if (ch == 'n') {AddCh(); goto case_138;}
            else {goto case_0;}
         case 138:
case_138:
            if (ch == 'g') {AddCh(); goto case_139;}
            else {goto case_0;}
         case 139:
case_139:
            recEnd = pos; recKind = 151;
            if (ch <= 9 || ch >= 11 && ch <= 12 || ch >= 14 && ch <= 65535) {AddCh(); goto case_139;}
            else {t->kind = 151; break;}
         case 140:
case_140:
            if (ch == 'e') {AddCh(); goto case_141;}
            else {goto case_0;}
         case 141:
case_141:
            if (ch == 'g') {AddCh(); goto case_142;}
            else {goto case_0;}
         case 142:
case_142:
            if (ch == 'i') {AddCh(); goto case_143;}
            else {goto case_0;}
         case 143:
case_143:
            if (ch == 'o') {AddCh(); goto case_144;}
            else {goto case_0;}
         case 144:
case_144:
            if (ch == 'n') {AddCh(); goto case_145;}
            else {goto case_0;}
         case 145:
case_145:
            recEnd = pos; recKind = 152;
            if (ch <= 9 || ch >= 11 && ch <= 12 || ch >= 14 && ch <= 65535) {AddCh(); goto case_145;}
            else {t->kind = 152; break;}
         case 146:
case_146:
            if (ch == 'e') {AddCh(); goto case_147;}
            else {goto case_0;}
         case 147:
case_147:
            if (ch == 'g') {AddCh(); goto case_148;}
            else {goto case_0;}
         case 148:
case_148:
            if (ch == 'i') {AddCh(); goto case_149;}
            else {goto case_0;}
         case 149:
case_149:
            if (ch == 'o') {AddCh(); goto case_150;}
            else {goto case_0;}
         case 150:
case_150:
            if (ch == 'n') {AddCh(); goto case_151;}
            else {goto case_0;}
         case 151:
case_151:
            recEnd = pos; recKind = 153;
            if (ch <= 9 || ch >= 11 && ch <= 12 || ch >= 14 && ch <= 65535) {AddCh(); goto case_151;}
            else {t->kind = 153; break;}
         case 152:
case_152:
            if (ch == 'r') {AddCh(); goto case_153;}
            else {goto case_0;}
         case 153:
case_153:
            if (ch == 'a') {AddCh(); goto case_154;}
            else {goto case_0;}
         case 154:
case_154:
            if (ch == 'g') {AddCh(); goto case_155;}
            else {goto case_0;}
         case 155:
case_155:
            if (ch == 'm') {AddCh(); goto case_156;}
            else {goto case_0;}
         case 156:
case_156:
            if (ch == 'a') {AddCh(); goto case_157;}
            else {goto case_0;}
         case 157:
case_157:
            recEnd = pos; recKind = 154;
            if (ch <= 9 || ch >= 11 && ch <= 12 || ch >= 14 && ch <= 65535) {AddCh(); goto case_157;}
            else {t->kind = 154; break;}
         case 158:
case_158:
            recEnd = pos; recKind = 2;
            if (ch >= '0' && ch <= '9') {AddCh(); goto case_158;}
            else if (ch == 'U') {AddCh(); goto case_172;}
            else if (ch == 'u') {AddCh(); goto case_173;}
            else if (ch == 'L') {AddCh(); goto case_174;}
            else if (ch == 'l') {AddCh(); goto case_175;}
            else if (ch == '.') {apx++; AddCh(); goto case_187;}
            else if (ch == 'E' || ch == 'e') {AddCh(); goto case_40;}
            else if (ch == 'D' || ch == 'F' || ch == 'M' || ch == 'd' || ch == 'f' || ch == 'm') {AddCh(); goto case_43;}
            else {t->kind = 2; break;}
         case 159:
            if (ch >= 'A' && ch <= 'Z' || ch == '_' || ch >= 'a' && ch <= 'z' || ch == 170 || ch == 181 || ch == 186 || ch >= 192 && ch <= 214 || ch >= 216 && ch <= 246 || ch >= 248 && ch <= 255) {AddCh(); goto case_1;}
            else if (ch == 92) {AddCh(); goto case_15;}
            else if (ch == '"') {AddCh(); goto case_76;}
            else {goto case_0;}
         case 160:
            recEnd = pos; recKind = 2;
            if (ch >= '0' && ch <= '9') {AddCh(); goto case_158;}
            else if (ch == 'U') {AddCh(); goto case_172;}
            else if (ch == 'u') {AddCh(); goto case_173;}
            else if (ch == 'L') {AddCh(); goto case_174;}
            else if (ch == 'l') {AddCh(); goto case_175;}
            else if (ch == '.') {apx++; AddCh(); goto case_187;}
            else if (ch == 'X' || ch == 'x') {AddCh(); goto case_29;}
            else if (ch == 'E' || ch == 'e') {AddCh(); goto case_40;}
            else if (ch == 'D' || ch == 'F' || ch == 'M' || ch == 'd' || ch == 'f' || ch == 'm') {AddCh(); goto case_43;}
            else {t->kind = 2; break;}
         case 161:
            recEnd = pos; recKind = 91;
            if (ch >= '0' && ch <= '9') {AddCh(); goto case_32;}
            else {t->kind = 91; break;}
         case 162:
            recEnd = pos; recKind = 83;
            if (ch == '=') {AddCh(); goto case_78;}
            else if (ch == '&') {AddCh(); goto case_104;}
            else {t->kind = 83; break;}
         case 163:
            recEnd = pos; recKind = 86;
            if (ch == '>') {AddCh(); goto case_79;}
            else if (ch == '=') {AddCh(); goto case_84;}
            else {t->kind = 86; break;}
         case 164:
            recEnd = pos; recKind = 87;
            if (ch == ':') {AddCh(); goto case_83;}
            else {t->kind = 87; break;}
         case 165:
            recEnd = pos; recKind = 94;
            if (ch == '=') {AddCh(); goto case_85;}
            else {t->kind = 94; break;}
         case 166:
            recEnd = pos; recKind = 110;
            if (ch == '+') {AddCh(); goto case_86;}
            else if (ch == '=') {AddCh(); goto case_96;}
            else {t->kind = 110; break;}
         case 167:
            recEnd = pos; recKind = 101;
            if (ch == '<') {AddCh(); goto case_188;}
            else if (ch == '=') {AddCh(); goto case_105;}
            else {t->kind = 101; break;}
         case 168:
            recEnd = pos; recKind = 107;
            if (ch == '=') {AddCh(); goto case_93;}
            else {t->kind = 107; break;}
         case 169:
            recEnd = pos; recKind = 112;
            if (ch == '?') {AddCh(); goto case_94;}
            else {t->kind = 112; break;}
         case 170:
            //           case_170:
            recEnd = pos; recKind = 118;
            if (ch == '=') {AddCh(); goto case_102;}
            else {t->kind = 118; break;}
         case 171:
case_171:
            if (ch == 9 || ch >= 11 && ch <= 12 || ch == ' ') {AddCh(); goto case_171;}
            else if (ch == 'd') {AddCh(); goto case_106;}
            else if (ch == 'u') {AddCh(); goto case_112;}
            else if (ch == 'i') {AddCh(); goto case_117;}
            else if (ch == 'e') {AddCh(); goto case_189;}
            else if (ch == 'l') {AddCh(); goto case_125;}
            else if (ch == 'w') {AddCh(); goto case_133;}
            else if (ch == 'r') {AddCh(); goto case_140;}
            else if (ch == 'p') {AddCh(); goto case_152;}
            else {goto case_0;}
         case 172:
case_172:
            recEnd = pos; recKind = 2;
            if (ch == 'L' || ch == 'l') {AddCh(); goto case_31;}
            else {t->kind = 2; break;}
         case 173:
case_173:
            recEnd = pos; recKind = 2;
            if (ch == 'L' || ch == 'l') {AddCh(); goto case_31;}
            else {t->kind = 2; break;}
         case 174:
case_174:
            recEnd = pos; recKind = 2;
            if (ch == 'U' || ch == 'u') {AddCh(); goto case_31;}
            else {t->kind = 2; break;}
         case 175:
case_175:
            recEnd = pos; recKind = 2;
            if (ch == 'U' || ch == 'u') {AddCh(); goto case_31;}
            else {t->kind = 2; break;}
         case 176:
case_176:
            recEnd = pos; recKind = 2;
            if (ch == 'L' || ch == 'l') {AddCh(); goto case_31;}
            else {t->kind = 2; break;}
         case 177:
case_177:
            recEnd = pos; recKind = 2;
            if (ch == 'L' || ch == 'l') {AddCh(); goto case_31;}
            else {t->kind = 2; break;}
         case 178:
case_178:
            recEnd = pos; recKind = 2;
            if (ch == 'U' || ch == 'u') {AddCh(); goto case_31;}
            else {t->kind = 2; break;}
         case 179:
case_179:
            recEnd = pos; recKind = 2;
            if (ch == 'U' || ch == 'u') {AddCh(); goto case_31;}
            else {t->kind = 2; break;}
         case 180:
case_180:
            if (ch == '"' || ch == 39 || ch == '0' || ch == 92 || ch >= 'a' && ch <= 'b' || ch == 'f' || ch == 'n' || ch == 'r' || ch == 't' || ch == 'v') {AddCh(); goto case_45;}
            else if (ch == 'x') {AddCh(); goto case_46;}
            else if (ch == 'u') {AddCh(); goto case_48;}
            else if (ch == 'U') {AddCh(); goto case_52;}
            else {goto case_0;}
         case 181:
case_181:
            if (ch >= '0' && ch <= '9' || ch >= 'A' && ch <= 'F' || ch >= 'a' && ch <= 'f') {AddCh(); goto case_182;}
            else if (ch == 39) {AddCh(); goto case_60;}
            else {goto case_0;}
         case 182:
case_182:
            if (ch >= '0' && ch <= '9' || ch >= 'A' && ch <= 'F' || ch >= 'a' && ch <= 'f') {AddCh(); goto case_45;}
            else if (ch == 39) {AddCh(); goto case_60;}
            else {goto case_0;}
         case 183:
case_183:
            if (ch == '"' || ch == 39 || ch == '0' || ch == 92 || ch >= 'a' && ch <= 'b' || ch == 'f' || ch == 'n' || ch == 'r' || ch == 't' || ch == 'v') {AddCh(); goto case_61;}
            else if (ch == 'x') {AddCh(); goto case_62;}
            else if (ch == 'u') {AddCh(); goto case_64;}
            else if (ch == 'U') {AddCh(); goto case_68;}
            else {goto case_0;}
         case 184:
case_184:
            if (ch >= '0' && ch <= '9' || ch >= 'A' && ch <= 'F' || ch >= 'a' && ch <= 'f') {AddCh(); goto case_185;}
            else if (ch <= 9 || ch >= 11 && ch <= 12 || ch >= 14 && ch <= '!' || ch >= '#' && ch <= '/' || ch >= ':' && ch <= '@' || ch >= 'G' && ch <= '[' || ch >= ']' && ch <= '`' || ch >= 'g' && ch <= 65535) {AddCh(); goto case_61;}
            else if (ch == '"') {AddCh(); goto case_77;}
            else if (ch == 92) {AddCh(); goto case_183;}
            else {goto case_0;}
         case 185:
case_185:
            if (ch <= 9 || ch >= 11 && ch <= 12 || ch >= 14 && ch <= '!' || ch >= '#' && ch <= '[' || ch >= ']' && ch <= 65535) {AddCh(); goto case_61;}
            else if (ch == '"') {AddCh(); goto case_77;}
            else if (ch == 92) {AddCh(); goto case_183;}
            else {goto case_0;}
         case 186:
case_186:
            recEnd = pos; recKind = 5;
            if (ch == '"') {AddCh(); goto case_76;}
            else {t->kind = 5; break;}
         case 187:
case_187:
            if (ch <= '/' || ch >= ':' && ch <= 65535) {apx++; AddCh(); goto case_28;}
            else if (ch >= '0' && ch <= '9') {apx = 0; AddCh(); goto case_36;}
            else {goto case_0;}
         case 188:
case_188:
            recEnd = pos; recKind = 102;
            if (ch == '=') {AddCh(); goto case_90;}
            else {t->kind = 102; break;}
         case 189:
case_189:
            if (ch == 'l') {AddCh(); goto case_190;}
            else if (ch == 'n') {AddCh(); goto case_191;}
            else if (ch == 'r') {AddCh(); goto case_129;}
            else {goto case_0;}
         case 190:
case_190:
            if (ch == 'i') {AddCh(); goto case_119;}
            else if (ch == 's') {AddCh(); goto case_121;}
            else {goto case_0;}
         case 191:
case_191:
            if (ch == 'd') {AddCh(); goto case_192;}
            else {goto case_0;}
         case 192:
case_192:
            if (ch == 'i') {AddCh(); goto case_123;}
            else if (ch == 'r') {AddCh(); goto case_146;}
            else {goto case_0;}
         case 193:
case_193:
            {t->kind = 136; break;}
         case 194:
case_194:
            {t->kind = 141; break;}
         case 195:
            recEnd = pos; recKind = 103;
            if (ch == '-') {AddCh(); goto case_81;}
            else if (ch == '=') {AddCh(); goto case_91;}
            else if (ch == '>') {AddCh(); goto case_194;}
            else {t->kind = 103; break;}
         case 196:
            recEnd = pos; recKind = 139;
            if (ch == '=') {AddCh(); goto case_82;}
            else {t->kind = 139; break;}
         case 197:
            recEnd = pos; recKind = 140;
            if (ch == '=') {AddCh(); goto case_92;}
            else {t->kind = 140; break;}
         case 198:
            recEnd = pos; recKind = 137;
            if (ch == '=') {AddCh(); goto case_95;}
            else if (ch == '|') {AddCh(); goto case_193;}
            else {t->kind = 137; break;}
         case 199:
            recEnd = pos; recKind = 138;
            if (ch == '=') {AddCh(); goto case_103;}
            else {t->kind = 138; break;}

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