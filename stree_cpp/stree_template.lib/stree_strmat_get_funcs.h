#ifndef _STREE_STRMAT_GET_FUNCS_H_
#define _STREE_STRMAT_GET_FUNCS_H_

//
//   Внимание! Нужно переделать всякие функции, которые выделяют память или явно используют тип char
//
//  И ЕЩЁ НУЖНО ЧТО-ТО СДЕЛАТЬ С return children[(int) ch];
//
//


#include "stree_strmat_structs.h"

//
//* Find the child of a node whose edge label begins with the character given
//* as a parameter.
//*
//* Parameters:  tree  -  a suffix tree
//*              node  -  a tree node
//*              ch    -  a character
//*
//* Returns:  a tree node or NULL.
//*/
template<typename T>
stree_node<T>* stree_find_child(suffix_tree<T>* tree, stree_node<T>* node, const T& ch)
{
   stree_node<T>* child;
   stree_node<T>** children;

   if (node->isaleaf || node->children == NULL)
      return NULL;

   if (!node->isanarray)
   {
      for (child=node->children; child != NULL; child=child->next)
      {
         T& childch = *(child->edgestr);

         if (ch == childch)
            return child;
         else
            if (tree->build_type == SORTED_LIST && ch < childch)
               return NULL;
      }
      return NULL;
   }
   else
   {
      children = (stree_node<T>**)node->children;
      return children[(int)ch];
   }
}

template<typename T>
int stree_get_num_children(suffix_tree<T>* tree, stree_node<T>* node)
{
   int i, count;
   stree_node<T>* child;
   stree_node<T>** children;

   if (node->isaleaf || node->children == NULL)
      return 0;

   if (!node->isanarray)
   {
      count = 0;
      for (child=node->children; child != NULL; child=child->next)
         count++;
   }
   else
   {
      count = 0;
      children = (stree_node<T>**) node->children;
      for (i=0; i < tree->alpha_size; i++)
         if (children[i] != NULL)
            count++;
   }

   return count;
}

//
//* Returns a linked list of the children of a suffix tree node.
//*
//* Parameters:  tree  -  a suffix tree
//*              node  -  a tree node
//*
//* Returns: the head of the list of children;
//*/
template<typename T>
stree_node<T>* stree_get_children(suffix_tree<T>* tree, stree_node<T>* node)
{
   if (node->isaleaf || node->children == NULL)
      return NULL;
   else if (!node->isanarray)
      return node->children;

   stree_node<T>* head = NULL;
   stree_node<T>* tail = NULL;
   stree_node<T>** children = (stree_node<T>**) node->children;
   for (int i=0; i < tree->alpha_size; i++)
   {
      if (children[i] != NULL)
      {
         if (head == NULL)
            head = tail = children[i];
         else
            tail = tail->next = children[i];
      }
   }
   if(tail) tail->next = NULL;
   return head;
}

///*
//* Get the length of the string labelling the path from the root to
//* a tree node.
//*
//* Parameters:  tree  -  a suffix tree
//*              node  -  a tree node
//*
//* Returns:  the length of the node's label.
//*/
template<typename T>
int stree_get_labellen(suffix_tree<T>* tree, stree_node<T>* node)
{
   int len = 0;
   while (node != stree_get_root(tree))
   {
      len += stree_get_edgelen(tree, node);
      node = stree_get_parent(tree, node);
   }
   return len;
}

//
//* Return the number of suffices that end at the tree node (these are the
//* "leaves" in the theoretical suffix tree).
//*
//* Parameters:  tree  -  a suffix tree
//*              node  -  a tree node
//*
//* Returns:  the number of "leaves" at that node.
//*/
template<typename T>
int stree_get_num_leaves(stree_node<T>* node)
{
   stree_intleaf* intleaf;

   if (node->isaleaf)
      return 1;
   else
   {
      int i = 0;
      intleaf = int_stree_get_intleaves(tree, node);
      for (i=0; intleaf != NULL; i++)
         intleaf = intleaf->next;
      return i;
   }
}

//
//* Get the sequence information about one of the suffices that end at
//* a tree node.  The `leafnum' parameter gives a number between 1 and
//* the number of "leaves" at the node, and information about that "leaf"
//* is returned.
//*
//* NOTE:  There is no ordering of the "leaves" at a node, either by
//*        string identifier number or by position.  You get whatever
//*        order the construction algorithm adds them to the node.
//*
//* Parameters:  tree        -  a suffix tree
//*              node        -  a tree node
//*              leafnum     -  which leaf to return
//*              string_out  -  address where to store the suffix pointer
//*                                (the value stored there points to the 
//*                                 beginning of the sequence containing the
//*                                 suffix, not the beginning of the suffix)
//*              pos_out     -  address where to store the position of the
//*                             suffix in the sequence
//*                                (a value between 1 and the sequence length)
//*              id_out      -  address where to store the seq. identifier
//*
//* Returns:  non-zero if a leaf was returned (i.e., the `leafnum' value
//*           referred to a valid leaf), and zero otherwise.
//*           NOTE: If no leaf is returned, *string_out, *pos_out and *id_out
//*                 are left untouched.
//*/
template<typename T>    
int stree_get_leaf(suffix_tree<T>* tree, stree_node<T>* node, int leafnum, T **string_out, int *pos_out, int *id_out)
{
   stree_intleaf* intleaf;
   stree_leaf<T>* leaf;

   if (node->isaleaf)
   {
      if (leafnum != 1)
         return 0;

      leaf = (stree_leaf<T>*)node;
      *string_out = int_stree_get_string(tree, leaf->strid);
      *pos_out = leaf->pos;
      *id_out = int_stree_get_strid(tree, leaf->strid);
      return 1;
   }
   else
   {
      intleaf = int_stree_get_intleaves(tree, node);
      for (int i=1; intleaf != NULL; i++,intleaf=intleaf->next)
      {
         if (i == leafnum)
         {
            *string_out = int_stree_get_string(tree, intleaf->strid);
            *pos_out = intleaf->pos;
            *id_out = int_stree_get_strid(tree, intleaf->strid);
            return 1;
         }
      }
      return 0;
   }
}

#endif // _STREE_STRMAT_GET_FUNCS_H_