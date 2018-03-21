#include "stree_strmat_deleting.h"
#include <malloc.h>

void int_stree_free_intleaf(stree_intleaf* ileaf)
{
   free(ileaf);
}