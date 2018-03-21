#if !defined(COCO_COMMENT_H__)
#define COCO_COMMENT_H__

#include <wchar.h>

namespace Coco
{
   namespace Cpp
   {
      class Comment  					// info about comment syntax
      {
      public:
         wchar_t* start;
         wchar_t* stop;
         bool nested;
         Comment *next;

         Comment(wchar_t* start, wchar_t* stop, bool nested);
         virtual ~Comment();

      };
   }
}

#endif // !defined(COCO_COMMENT_H__)