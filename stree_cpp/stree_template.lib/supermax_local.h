#pragma once

#include <list>
#include <vector>
//#include "stree_strmat.h"
//#include "repeats_supermax.h"

/*
*
*
* The data structure to hold the left predecessors of leaves in each
* nodes sub-tree.  Declared as an array of linked lists (indexed on
* the suffix tree node identifiers).
*
*
*/
struct lvlist
{
   typedef std::list<lvlist> list;
   typedef std::vector<list> container;

   int value;
   int count;
   lvlist* next;

   lvlist()
      : value(0), count(0)
   {}

   lvlist(int _value, int _count)
      : value(_value), count(_count)
   {}
};

void lvals_init(int num_nodes);
bool lvals_add_value(int id, int value, int amount);
int lvals_get_value(int id, int value);
lvlist::list& get_lvals(int id);