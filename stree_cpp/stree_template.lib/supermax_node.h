#pragma once

#include <vector>
#include <list>

struct supermax_node
{
   typedef std::list<supermax_node> nodes;
   typedef std::vector<int> Positions;
   typedef Positions::const_iterator PositionsIter;

   int M;
   int num_witness;
   int num_leaves;
   int percent;
   Positions pos;

   supermax_node(int supermax_len, int witness, int leaves, const Positions& poses)
      : M(supermax_len), num_witness(witness), num_leaves(leaves), percent(0), pos(poses)
   {
      percent = (int) (((float)num_witness) / ((float)num_leaves) * 100.0);
   }

   int get_ending(int position) const
   {
      return (position + M - 1);
   }
};