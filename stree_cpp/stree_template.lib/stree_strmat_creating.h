#ifndef _STREE_STRMAT_CREATING_H_
#define _STREE_STRMAT_CREATING_H_

#include "stree_strmat_structs.h"

stree_intleaf* int_stree_new_intleaf(int strid, int pos);

///*
//* Allocates memory for a new LEAF structure.
//*
//* Parameters:  tree        -  A suffix tree
//*              strid       -  The id of the string containing the new suffix
//*                             to be added to the tree.
//*              edgepos     -  The position of the edge label in the string.
//*              leafpos     -  The position of the new suffix in the string.
//*
//* Returns:  The structure or NULL.
//*/
template<typename T>
stree_leaf<T>* int_stree_new_leaf(suffix_tree<T>* tree, int strid, int edgepos, int leafpos)
{
   stree_leaf<T>* leaf;

   if ((leaf = (stree_leaf<T>*)malloc(sizeof(stree_leaf<T>))) == NULL)
      return NULL;

   leaf->init();
   leaf->isaleaf = 1;
   leaf->strid = strid;
   leaf->pos = leafpos;
   leaf->edgestr = int_stree_get_string(tree, strid) + edgepos;
   leaf->rawedgestr = int_stree_get_rawstring(tree, strid) + edgepos;
   leaf->edgelen = int_stree_get_length(tree, strid) - edgepos;

   return leaf;
}

//
//* Allocates memory for a new NODE structure.
//*
//* Parameters:  tree        -  A suffix tree
//*              edgestr     -  The edge label on the edge to the node.
//*              rawedgestr  -  The raw edge label on the edge to the leaf.
//*              edgelen     -  The edge label's length.
//*
//* Returns:  The structure or NULL.
//*/
template<typename T>
stree_node<T>* int_stree_new_node(suffix_tree<T>* tree, T *edgestr, T *rawedgestr, int edgelen)
{
   stree_node<T>* node = NULL;

   if ((node = (stree_node<T>*)malloc(sizeof(stree_node<T>))) == NULL)
      return NULL;

   node->init();
   node->edgestr = edgestr;
   node->rawedgestr = rawedgestr;
   node->edgelen = edgelen;

   if (tree->build_type == COMPLETE_ARRAY)
   {
      node->children = (stree_node<T>*)malloc(tree->alpha_size * sizeof(stree_node<T>*));
      if (node->children == NULL)
      {
         free(node);
         return NULL;
      }

      memset(node->children, 0, tree->alpha_size * sizeof(stree_node<T>*));
      node->isanarray = 1;
   }

   return node;
}

//
//* Allocates a new suffix tree data structure.
//*
//* Parameters:  alphasize   -  the size of the alphabet
//*                               (all strings are assumed to use "characters"
//*                                in the range of 0..alphasize-1)
//*              copyflag    -  make a copy of each sequence?
//*              build_type  -  what type of data structure should be used
//*                             to store pointers to the children of a node
//*              threshold   -  With the LIST_THEN_ARRAY structure, what
//*                             is the threshold for converting from the
//*                             list to the array
//*
//* Returns:  A suffix_tree* structure
//*/
template<typename T>
suffix_tree<T>* stree_new_tree(int alphasize, int copyflag, int build_type, int build_threshold)
{
   suffix_tree<T>* tree = NULL;

   if (alphasize <= 0 || alphasize > MAX_ALPHABET_SIZE)
      return NULL;

   if ((build_type != LINKED_LIST && build_type != SORTED_LIST &&
      build_type != LIST_THEN_ARRAY && build_type != COMPLETE_ARRAY) ||
      (build_type == LIST_THEN_ARRAY && build_threshold <= 0))
      return NULL;

   /*
   * Allocate the space.
   */
   if ((tree = (suffix_tree<T>*)malloc(sizeof(suffix_tree<T>))) == NULL)
      return NULL;

   memset(tree, 0, sizeof(suffix_tree<T>));
   tree->copyflag = copyflag;
   tree->alpha_size = alphasize;
   tree->build_threshold = build_threshold;
   tree->build_type = build_type;

   T* nul = NULL;
   if ((tree->root = int_stree_new_node(tree, nul, nul, 0)) == NULL)
   {
      free(tree);
      return NULL;
   }
   tree->num_nodes = 1;
   return tree;
}

//
//* Connects an intleaf to a node.
//*
//* Parameters:  tree  -  A suffix tree
//*              node  -  A tree node
//*              id    -  The internal identifier of the string
//*              pos   -  The position of the suffix in the string
//*
//* Returns:  Non-zero on success, zero on error.
//*/
template<typename T>
int int_stree_add_intleaf(stree_node<T>* node, int strid, int pos)
{
   stree_intleaf* intleaf;

   if (node->isaleaf || (intleaf = int_stree_new_intleaf(strid, pos)) == NULL)
      return 0;

   intleaf->next = node->leaves;
   node->leaves = intleaf;
   return 1;
}

#endif // _STREE_STRMAT_CREATING_H_