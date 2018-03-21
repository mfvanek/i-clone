#include "supermax_local.h"

static lvlist::container stack_;

void lvals_init(int num_nodes)
{
   stack_ = lvlist::container(num_nodes);
}

bool lvals_add_value(int id, int value, int amount)
{
   lvlist::list& lst = stack_[id];
   for(lvlist::list::iterator it = lst.begin(); it != lst.end(); ++it)
   {
      if(it->value == value)
      {
         it->count += amount;
         return true;
      }
   }

   lst.push_front(lvlist(value, amount));
   return true;
}

int lvals_get_value(int id, int value)
{
   lvlist::list& lst = stack_[id];
   for(lvlist::list::iterator it = lst.begin(); it != lst.end(); ++it)
   {
      if(it->value == value)
         return it->count;
   }
   return 0;
}

lvlist::list& get_lvals(int id)
{
   return stack_[id];
}

// EOF