#include <memory.h>
#include <string.h>
#include "coco_common.h"

namespace Coco
{
   // string handling, wide character
   wchar_t* coco_string_create(const wchar_t* value)
   {
      return coco_string_create(value, 0);
   }

   wchar_t* coco_string_create(const wchar_t *value, int startIndex)
   {
      int valueLen = 0;
      int len = 0;

      if (value) {
         valueLen = wcslen(value);
         len = valueLen - startIndex;
      }

      return coco_string_create(value, startIndex, len);
   }

#pragma warning(disable: 4996)
   wchar_t* coco_string_create(const wchar_t *value, int startIndex, int length)
   {
      int len = 0;
      wchar_t* data;
      if (value) { len = length; }
      data = new wchar_t[len + 1];
      wcsncpy(data, &(value[startIndex]), len);
      data[len] = 0;
      return data;
   }
#pragma warning(default: 4996)

   wchar_t* coco_string_create_upper(const wchar_t* data) {
      if (!data) { return NULL; }

      int dataLen = 0;
      if (data) { dataLen = wcslen(data); }

      wchar_t *newData = new wchar_t[dataLen + 1];

      for (int i = 0; i <= dataLen; i++) {
         if ((L'a' <= data[i]) && (data[i] <= L'z')) {
            newData[i] = data[i] + (L'A' - L'a');
         }
         else { newData[i] = data[i]; }
      }

      newData[dataLen] = L'\0';
      return newData;
   }

   wchar_t* coco_string_create_lower(const wchar_t* data) {
      if (!data) { return NULL; }
      int dataLen = wcslen(data);
      return coco_string_create_lower(data, 0, dataLen);
   }

   wchar_t* coco_string_create_lower(const wchar_t* data, int startIndex, int dataLen) {
      if (!data) { return NULL; }

      wchar_t* newData = new wchar_t[dataLen + 1];

      for (int i = 0; i <= dataLen; i++) {
         wchar_t ch = data[startIndex + i];
         if ((L'A' <= ch) && (ch <= L'Z')) {
            newData[i] = ch - (L'A' - L'a');
         }
         else { newData[i] = ch; }
      }
      newData[dataLen] = L'\0';
      return newData;
   }

#pragma warning(disable: 4996)
   wchar_t* coco_string_create_append(const wchar_t* data1, const wchar_t* data2)
   {
      wchar_t* data;
      int data1Len = 0;
      int data2Len = 0;

      if (data1) { data1Len = wcslen(data1); }
      if (data2) {data2Len = wcslen(data2); }

      data = new wchar_t[data1Len + data2Len + 1];

      if (data1) { wcscpy(data, data1); }
      if (data2) { wcscpy(data + data1Len, data2); }

      data[data1Len + data2Len] = 0;

      return data;
   }
#pragma warning(default: 4996)

#pragma warning(disable: 4996)
   wchar_t* coco_string_create_append(const wchar_t *target, const wchar_t appendix) {
      int targetLen = coco_string_length(target);
      wchar_t* data = new wchar_t[targetLen + 2];
      wcsncpy(data, target, targetLen);
      data[targetLen] = appendix;
      data[targetLen + 1] = 0;
      return data;
   }
#pragma warning(default: 4996)

   void coco_string_delete(wchar_t* &data) {
      delete [] data;
      data = NULL;
   }

   int coco_string_length(const wchar_t* data) {
      if (data) { return wcslen(data); }
      return 0;
   }

   bool coco_string_endswith(const wchar_t* data, const wchar_t *end) {
      int dataLen = wcslen(data);
      int endLen = wcslen(end);
      return (endLen <= dataLen) && (wcscmp(data + dataLen - endLen, end) == 0);
   }

   int coco_string_indexof(const wchar_t* data, const wchar_t value) {
      const wchar_t* chr = wcschr(data, value);

      if (chr) { return (chr-data); }
      return -1;
   }

   int coco_string_lastindexof(const wchar_t* data, const wchar_t value) {
      const wchar_t* chr = wcsrchr(data, value);

      if (chr) { return (chr-data); }
      return -1;
   }

   void coco_string_merge(wchar_t* &target, const wchar_t* appendix) {
      if (!appendix) { return; }
      wchar_t* data = coco_string_create_append(target, appendix);
      delete [] target;
      target = data;
   }

   bool coco_string_equal(const wchar_t* data1, const wchar_t* data2) {
      return wcscmp( data1, data2 ) == 0;
   }

   int coco_string_compareto(const wchar_t* data1, const wchar_t* data2) {
      return wcscmp(data1, data2);
   }

   int coco_string_hash(const wchar_t *data) {
      int h = 0;
      if (!data) { return 0; }
      while (*data != 0) {
         h = (h * 7) ^ *data;
         ++data;
      }
      if (h < 0) { h = -h; }
      return h;
   }

   // string handling, ascii character

   wchar_t* coco_string_create(const char* value) {
      int len = 0;
      if (value) { len = strlen(value); }
      wchar_t* data = new wchar_t[len + 1];
      for (int i = 0; i < len; ++i) { data[i] = (wchar_t) value[i]; }
      data[len] = 0;
      return data;
   }

   char* coco_string_create_char(const wchar_t *value) {
      int len = coco_string_length(value);
      char *res = new char[len + 1];
      for (int i = 0; i < len; ++i) { res[i] = (char) value[i]; }
      res[len] = 0;
      return res;
   }

   void coco_string_delete(char* &data) {
      delete [] data;
      data = NULL;
   }

   std::string string_create_char(const wchar_t *value)
   {
      char *str = coco_string_create_char(value);
      std::string result = str;
      coco_string_delete(str);
      return result;
   }


   Token::Token()
   {
      kind = 0;
      pos  = 0;
      col  = 0;
      line = 0;
      val  = NULL;
      next = NULL;
   }

   Token::~Token()
   {
      coco_string_delete(val);
   }

   Buffer::Buffer(FILE* s, bool isUserStream)
   {
      // ensure binary read on windows
#if _MSC_VER >= 1300
      _setmode(_fileno(s), _O_BINARY);
#endif
      stream = s; this->isUserStream = isUserStream;
      if (CanSeek()) {
         fseek(s, 0, SEEK_END);
         fileLen = ftell(s);
         fseek(s, 0, SEEK_SET);
         bufLen = (fileLen < COCO_MAX_BUFFER_LENGTH) ? fileLen : COCO_MAX_BUFFER_LENGTH;
         bufStart = INT_MAX; // nothing in the buffer so far
      } else {
         fileLen = bufLen = bufStart = 0;
      }
      bufCapacity = (bufLen>0) ? bufLen : COCO_MIN_BUFFER_LENGTH;
      buf = new unsigned char[bufCapacity];	
      if (fileLen > 0) SetPos(0);          // setup  buffer to position 0 (start)
      else bufPos = 0; // index 0 is already after the file, thus Pos = 0 is invalid
      if (bufLen == fileLen && CanSeek()) Close();
   }

   Buffer::Buffer(Buffer *b) {
      buf = b->buf;
      bufCapacity = b->bufCapacity;
      b->buf = NULL;
      bufStart = b->bufStart;
      bufLen = b->bufLen;
      fileLen = b->fileLen;
      bufPos = b->bufPos;
      stream = b->stream;
      b->stream = NULL;
      isUserStream = b->isUserStream;
   }

   Buffer::Buffer(const unsigned char* buf, int len) {
      this->buf = new unsigned char[len];
      memcpy(this->buf, buf, len*sizeof(unsigned char));
      bufStart = 0;
      bufCapacity = bufLen = len;
      fileLen = len;
      bufPos = 0;
      stream = NULL;
   }

   Buffer::~Buffer() {
      Close(); 
      if (buf != NULL) {
         delete [] buf;
         buf = NULL;
      }
   }

   void Buffer::Close() {
      if (!isUserStream && stream != NULL) {
         fclose(stream);
         stream = NULL;
      }
   }

   int Buffer::Read() {
      if (bufPos < bufLen) {
         return buf[bufPos++];
      } else if (GetPos() < fileLen) {
         SetPos(GetPos()); // shift buffer start to Pos
         return buf[bufPos++];
      } else if ((stream != NULL) && !CanSeek() && (ReadNextStreamChunk() > 0)) {
         return buf[bufPos++];
      } else {
         return EoF;
      }
   }

   int Buffer::Peek() {
      int curPos = GetPos();
      int ch = Read();
      SetPos(curPos);
      return ch;
   }

   // beg .. begin, zero-based, inclusive, in byte
   // end .. end, zero-based, exclusive, in byte
   wchar_t* Buffer::GetString(int beg, int end) {
      int len = 0;
      wchar_t *buf = new wchar_t[end - beg];
      int oldPos = GetPos();
      SetPos(beg);
      while (GetPos() < end) buf[len++] = (wchar_t) Read();
      SetPos(oldPos);
      wchar_t *res = coco_string_create(buf, 0, len);
      coco_string_delete(buf);
      return res;
   }

   int Buffer::GetPos() {
      return bufPos + bufStart;
   }

   void Buffer::SetPos(int value) {
      if ((value >= fileLen) && (stream != NULL) && !CanSeek()) {
         // Wanted position is after buffer and the stream
         // is not seek-able e.g. network or console,
         // thus we have to read the stream manually till
         // the wanted position is in sight.
         while ((value >= fileLen) && (ReadNextStreamChunk() > 0));
      }

      if ((value < 0) || (value > fileLen)) {
         wprintf(L"--- buffer out of bounds access, position: %d\n", value);
         exit(1);
      }

      if ((value >= bufStart) && (value < (bufStart + bufLen))) { // already in buffer
         bufPos = value - bufStart;
      } else if (stream != NULL) { // must be swapped in
         fseek(stream, value, SEEK_SET);
         bufLen = fread(buf, sizeof(unsigned char), bufCapacity, stream);
         bufStart = value; bufPos = 0;
      } else {
         bufPos = fileLen - bufStart; // make Pos return fileLen
      }
   }

   // Read the next chunk of bytes from the stream, increases the buffer
   // if needed and updates the fields fileLen and bufLen.
   // Returns the number of bytes read.
   int Buffer::ReadNextStreamChunk() {
      int free = bufCapacity - bufLen;
      if (free == 0) {
         // in the case of a growing input stream
         // we can neither seek in the stream, nor can we
         // foresee the maximum length, thus we must adapt
         // the buffer size on demand.
         bufCapacity = bufLen * 2;
         unsigned char *newBuf = new unsigned char[bufCapacity];
         memcpy(newBuf, buf, bufLen*sizeof(unsigned char));
         delete [] buf;
         buf = newBuf;
         free = bufLen;
      }
      int read = fread(buf + bufLen, sizeof(unsigned char), free, stream);
      if (read > 0) {
         fileLen = bufLen = (bufLen + read);
         return read;
      }
      // end of stream reached
      return 0;
   }

   bool Buffer::CanSeek() {
      return (stream != NULL) && (ftell(stream) != -1);
   }

   int UTF8Buffer::Read()
   {
      int ch;
      do {
         ch = Buffer::Read();
         // until we find a utf8 start (0xxxxxxx or 11xxxxxx)
      } while ((ch >= 128) && ((ch & 0xC0) != 0xC0) && (ch != EoF));
      if (ch < 128 || ch == EoF) {
         // nothing to do, first 127 chars are the same in ascii and utf8
         // 0xxxxxxx or end of file character
      } else if ((ch & 0xF0) == 0xF0) {
         // 11110xxx 10xxxxxx 10xxxxxx 10xxxxxx
         int c1 = ch & 0x07; ch = Buffer::Read();
         int c2 = ch & 0x3F; ch = Buffer::Read();
         int c3 = ch & 0x3F; ch = Buffer::Read();
         int c4 = ch & 0x3F;
         ch = (((((c1 << 6) | c2) << 6) | c3) << 6) | c4;
      } else if ((ch & 0xE0) == 0xE0) {
         // 1110xxxx 10xxxxxx 10xxxxxx
         int c1 = ch & 0x0F; ch = Buffer::Read();
         int c2 = ch & 0x3F; ch = Buffer::Read();
         int c3 = ch & 0x3F;
         ch = (((c1 << 6) | c2) << 6) | c3;
      } else if ((ch & 0xC0) == 0xC0) {
         // 110xxxxx 10xxxxxx
         int c1 = ch & 0x1F; ch = Buffer::Read();
         int c2 = ch & 0x3F;
         ch = (c1 << 6) | c2;
      }
      return ch;
   }
}