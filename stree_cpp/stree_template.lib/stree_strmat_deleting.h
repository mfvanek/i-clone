#ifndef _STREE_STRMAT_DELETING_H_
#define _STREE_STRMAT_DELETING_H_

#include "stree_strmat_structs.h"

void int_stree_free_intleaf(stree_intleaf* ileaf);

template<typename T>
void int_stree_free_leaf(stree_leaf<T>* leaf)
{
   free(leaf);
}

template<typename T>
void int_stree_free_node(stree_node<T>* node)
{
   if (node->isanarray)
   {
      free(node->children);
   }
   free(node);
}

template<typename T>
void int_stree_delete_subtree(suffix_tree<T>* tree, stree_node<T>* node)
{
   stree_node<T>* child;
   stree_node<T>* temp;
   stree_node<T>** children;
   stree_intleaf* ileaf;
   stree_intleaf* itemp;

   if (node->isaleaf)
      int_stree_free_leaf((stree_leaf<T>*)node);
   else
   {
      for (ileaf=node->leaves; ileaf != NULL; ileaf=itemp)
      {
         itemp = ileaf->next;
         int_stree_free_intleaf(ileaf);
      }

      if (!node->isanarray)
      {
         for (child=node->children; child != NULL; child=temp)
         {
            temp = child->next;
            int_stree_delete_subtree(tree, child);
         }
      }
      else
      {
         children = (stree_node<T>**) node->children;
         for (int i=0; i < tree->alpha_size; i++)
            if (children[i] != NULL)
               int_stree_delete_subtree(tree, children[i]);
      }

      int_stree_free_node(node);
   }
}

template<typename T>
void stree_delete_tree(suffix_tree<T>* tree)
{
   int i;

   int_stree_delete_subtree(tree, stree_get_root(tree));

   if (tree->strings != NULL)
   {
      if (tree->copyflag)
      {
         for (i=0; i < tree->strsize; i++)
            if (tree->strings[i] != NULL)
               free(tree->strings[i]);
      }
      free(tree->strings);
   }
   if (tree->rawstrings != NULL)
   {
      if (tree->copyflag)
      {
         for (i=0; i < tree->strsize; i++)
            if (tree->rawstrings[i] != NULL)
               free(tree->rawstrings[i]);
      }
      free(tree->rawstrings);
   }
   if (tree->ids != NULL)
      free(tree->ids);
   if (tree->lengths != NULL)
      free(tree->lengths);

   free(tree);
}

#endif // _STREE_STRMAT_DELETING_H_