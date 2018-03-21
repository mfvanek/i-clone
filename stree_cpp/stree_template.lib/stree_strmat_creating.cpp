#include "stree_strmat_creating.h"
#include <malloc.h>

stree_intleaf* int_stree_new_intleaf(int strid, int pos)
{
   stree_intleaf* ileaf;

   if ((ileaf = (stree_intleaf*)malloc(sizeof(stree_intleaf))) == NULL)
      return NULL;

   ileaf->init();
   ileaf->strid = strid;
   ileaf->pos = pos;

   return ileaf;
}