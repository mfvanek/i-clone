#ifndef _REPEATS_SUPERMAX_H_
#define _REPEATS_SUPERMAX_H_

#include "stree_strmat.h"
#include "stree_ukkonen.h"
#include "stree_strmat_creating.h"
#include "stree_strmat_deleting.h"
#include "supermax_local.h"
#include <stack>
#include "supermax_node.h"


//
//
//  Внимание!!! При заполнении left predecessors str[pos-1] приводится к int!!!!!
//
//
//

namespace Private
{
   template<typename T>
   static bool is_supermaximal_node(suffix_tree<T>* tree, stree_node<T>* node, int id, int K_min, supermax_node::nodes& list)
   {
      /*
      * Determine if the current node is a supermaximal or near supermaximal.
      *
      * First, find the total number of leaves in the sub-tree and the
      * number of different left predecessors of those leaves (i.e., the
      * diversity of the left predecessors).  Any node with a diversity
      * greater than 1 is "left diverse".
      */
      int num_leaves = 0;
      int diversity = 0;
      lvlist::list& lvalnode2 = get_lvals(id);
      for(lvlist::list::const_iterator it = lvalnode2.begin(); it != lvalnode2.end(); ++it)
      {
         num_leaves += it->count;
         diversity++;
      }

      if (diversity == 1)
         return false;

      /*
      * Next, find out how many of the leaves at the current node or
      * its children whose left predecessor is unique (i.e, no other leaf in
      * the subtree has the same left predecessor).  Each such leaf is a
      * witness to the supermaximality of the current node's label.
      */
      T *str = NULL;
      int pos = 0;
      int index = 0;
      int witnesses = 0;
      supermax_node::Positions positions; positions.reserve(5);
      stree_node<T>* child = stree_get_children(tree, node);
      while (child != NULL)
      {
         if (stree_get_num_children(tree, child) == 0 && stree_get_num_leaves(child) > 0)
         {
            for (int i=1; stree_get_leaf(tree, child, i, &str, &pos, &index); i++)
            {
               if (lvals_get_value(id, (pos == 0 ? MAX_ALPHABET_SIZE : str[pos-1])) == 1)
               {
                  witnesses++;
                  positions.push_back(pos);
               }
            }
         }
         child = stree_get_next(tree, child);
      }

      for (int i=1; stree_get_leaf(tree, node, i, &str, &pos, &index); i++)
      {
         if (lvals_get_value(id, (pos == 0 ? MAX_ALPHABET_SIZE : str[pos-1])) == 1)
         {
            witnesses++;
            positions.push_back(pos);
         }
      }

      if (witnesses == 0)
         return false;

      /*
      * Check whether the node is sufficiently a near supermaximal.
      */
      int supermaximal_len = stree_get_labellen(tree, node);
      if (supermaximal_len >= K_min && (witnesses == num_leaves))
      {
         supermax_node newnode(supermaximal_len, witnesses, num_leaves, positions);
         list.push_back(newnode);
      }
      return true;
   }

   template<typename T>
   static void SumUpNumsOfLeftPredecessors(suffix_tree<T>* tree, stree_node<T>* child, int id)
   {
      /*
      * Sum up the numbers of left predecessors computed for
      * the childrens' sub-trees.
      */
      lvlist::list& lvalnode2 = get_lvals(stree_get_ident(tree, child));
      for(lvlist::list::const_iterator it = lvalnode2.begin(); it != lvalnode2.end(); ++it)
      {
         lvals_add_value(id, it->value, it->count);
      }
   }

   template<typename T>
   static void AddInTheLeftPredecessors(suffix_tree<T>* tree, stree_node<T>* node, int id)
   {
      /*
      * Add in the left predecessors of any leaves of the current node.
      */
      T *str = NULL;
      int pos = 0;
      int index = 0;
      for (int i=1; stree_get_leaf(tree, node, i, &str, &pos, &index); i++)
         lvals_add_value(id, (pos == 0 ? MAX_ALPHABET_SIZE : str[pos-1]), 1);
   }

   template<typename T>
   void compute_supermax(suffix_tree<T>* tree, stree_node<T>* node, int K_min, supermax_node::nodes& list)
   {
      /*
      * Recurse on the children, computing the numbers of left predecessors
      * for suffixes in the sub-tree rooted at the current node.
      */
      int id = stree_get_ident(tree, node);
      stree_node<T>* child = stree_get_children(tree, node);
      while (child != NULL)
      {
         compute_supermax(tree, child, K_min, list);
         SumUpNumsOfLeftPredecessors(tree, child, id);
         child = stree_get_next(tree, child);
      }

      if (node != stree_get_root(tree))
      {
         AddInTheLeftPredecessors(tree, node, id);
         is_supermaximal_node(tree, node, id, K_min, list);
      }
   }
}

template<typename T>
void supermax_find(const std::vector<T>& Str, int K_min, supermax_node::nodes& clones)
{
   if (!Str.empty())
   {
      suffix_tree<T>* tree = NULL;
      int M = Str.size();

      // Построим суффиксное дерево и выделим необходимое количество памяти
      if ((tree = stree_new_tree<T>(MAX_ALPHABET_SIZE, 0, SORTED_LIST, 0)) != NULL)
      {
         if (stree_ukkonen_add_string(tree, &Str[0], &Str[0], M, 1) > 0)
         {
            lvals_init(stree_get_num_nodes(tree));
            // Найдем супермаксимальные повторы
            Private::compute_supermax(tree, stree_get_root(tree), K_min, clones);
         }
         stree_delete_tree(tree);
      }
   }
}

template<typename T>
supermax_node::nodes supermax_find(const std::vector<T>& Str, int K_min)
{
   supermax_node::nodes list;
   supermax_find(Str, K_min, list);
   return list;
}

#endif // _REPEATS_SUPERMAX_H_