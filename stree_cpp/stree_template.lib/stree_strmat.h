#ifndef _STREE_STRMAT_H_
#define _STREE_STRMAT_H_

//
//   Внимание! Нужно переделать всякие функции, которые выделяют память или явно используют тип char
//
//  И ЕЩЁ НУЖНО ЧТО-ТО СДЕЛАТЬ С return children[(int) ch];
//
//


#include "stree_strmat_structs.h"


/*
*
* Internal procedures to use when building and manipulating trees.
*
*/

//
//* Insert a string into the list of strings maintained in the 
//* suffix_tree* structure, in preparation for adding the suffixes of
//* the string to the tree.
//*
//* Parameters:  tree  -  A suffix tree
//*              S     -  The sequence
//*              Sraw  -  The raw sequence
//*              M     -  The sequence length
//*
//* Returns:  The internal index into the suffix_tree* structure's
//*           strings/rawstrings/lengths/ids arrays, or -1 on an error.
//*/
template<typename T>
int int_stree_insert_string(suffix_tree<T>* tree, const T *Str, const T *Sraw, int M, int strid)
{
   int i;
   int slot;
   int newsize;

   if (tree->nextslot == tree->strsize)
   {
      if (tree->strsize == 0)
      {
         tree->strsize = 128;
         if ((tree->strings = (T**)malloc(tree->strsize * sizeof(T*))) == NULL ||
            (tree->rawstrings = (T**)malloc(tree->strsize * sizeof(T*))) == NULL)
            return -1;
         if ((tree->lengths = (int*)malloc(tree->strsize * sizeof(int))) == NULL ||
            (tree->ids = (int*)malloc(tree->strsize * sizeof(int))) == NULL)
            return -1;
         for (i=0; i < 128; i++)
         {
            tree->strings[i] = tree->rawstrings[i] = NULL;
            tree->lengths[i] = tree->ids[i] = 0;
         }
      }
      else
      {
#pragma warning(disable: 6308)
         newsize = tree->strsize + tree->strsize;
         if ((tree->strings = (T**)realloc(tree->strings, tree->strsize * sizeof(T*))) == NULL ||
            (tree->rawstrings = (T**)realloc(tree->rawstrings, tree->strsize * sizeof(T*))) == NULL)
            return -1;
         if ((tree->lengths = (int*)realloc(tree->lengths, tree->strsize * sizeof(int))) == NULL ||
            (tree->ids = (int*)realloc(tree->ids, tree->strsize * sizeof(int))) == NULL)
            return -1;

         for (i=tree->strsize; i < newsize; i++)
         {
            tree->strings[i] = tree->rawstrings[i] = NULL;
            tree->lengths[i] = tree->ids[i] = 0;
         }
         tree->strsize = newsize;
#pragma warning(default: 6308)
      }
   }

   slot = tree->nextslot;
   tree->strings[slot] = (T*)Str;
   tree->rawstrings[slot] = (T*)Sraw;
   tree->lengths[slot] = M;
   tree->ids[slot] = strid;

   for (i=slot+1; i < tree->strsize; i++)
      if (tree->strings[i] == NULL)
         break;
   tree->nextslot = i;

   return slot;
}

//
//* Replaces one node with another in the suffix tree, reconnecting
//* the link from the parent to the new node.
//*
//* Parameters:  tree      -  A suffix tree
//*              parent    -  The parent of the node being replaced
//*              oldchild  -  The child being replaced
//*              newchild  -  The new child
//*
//* Returns:  nothing
//*/
template<typename T>
void int_stree_reconnect(suffix_tree<T>* tree, stree_node<T>* parent, stree_node<T>* oldchild, stree_node<T>* newchild)
{
   stree_node<T>* child;
   stree_node<T>* back;
   stree_node<T>** children;

   if (!parent->isanarray)
   {
      back = NULL;
      for (child=parent->children; child != oldchild; child=child->next)
         back = child;

      newchild->next = child->next;
      if (back == NULL)
         parent->children = newchild;
      else
         back->next = newchild;
   }
   else
   {
      children = (stree_node<T>**) parent->children;
      children[(int)(*(newchild->edgestr))] = newchild;
   }

   newchild->parent = parent;
   oldchild->parent = NULL;

   tree->idents_dirty = 1;
}

///*
//* int_stree_convert_leafnode
//*
//* Convert a LEAF structure into a NODE structure and replace the
//* NODE for the LEAF in the suffix tree..
//*
//* Parameters:  tree  -  a suffix tree
//*              node  -  a leaf of the tree
//*
//* Returns:  The NODE structure corresponding to the leaf, or NULL.
//*/
template<typename T>
stree_node<T>* int_stree_convert_leafnode(suffix_tree<T>* tree, stree_node<T>* node)
{
   stree_node<T>* newnode;
   stree_leaf<T>* leaf;
   stree_intleaf* ileaf;

   leaf = (stree_leaf<T>*)node;

   newnode = int_stree_new_node(tree, leaf->edgestr, leaf->rawedgestr, leaf->edgelen);
   if (newnode == NULL)
      return NULL;

   if ((ileaf = int_stree_new_intleaf(leaf->strid, leaf->pos)) == NULL)
   {
      int_stree_free_node(newnode);
      return NULL;
   }

   newnode->id = leaf->id;
   newnode->leaves = ileaf;

   int_stree_reconnect(tree, node->parent, node, newnode);
   int_stree_free_leaf(leaf);

   return newnode;
}

///*
//* int_stree_connect
//*
//* Connect a node as the child of another node.
//*
//* Parameters:  tree   -  A suffix tree
//*              parent -  The node to get the new child.
//*              child  -  The child being replaced
//*
//* Returns:  The parent after the child has been connected (if the
//*           parent was originally a leaf, this may mean replacing
//*           the leaf with a node).
//*/
template<typename T>
stree_node<T>* int_stree_connect(suffix_tree<T>* tree, stree_node<T>* parent, stree_node<T>* child)
{
   int count;
   T ch;
   stree_node<T>* temp;
   stree_node<T>* back;
   stree_node<T>** children;

   if (parent->isaleaf &&
      (parent = int_stree_convert_leafnode(tree, parent)) == NULL)
      return NULL;

   child->parent = parent;
   ch = *(child->edgestr);

   switch (tree->build_type)
   {
   case LINKED_LIST:
      child->next = parent->children;
      parent->children = child;
      break;

   case SORTED_LIST:
      back = NULL;
      for (temp=parent->children; temp != NULL; back=temp,temp=temp->next)
      {
         if (ch < (*(temp->edgestr)))
            break;
      }

      child->next = temp;
      if (back == NULL)
         parent->children = child;
      else
         back->next = child;
      break;

   case LIST_THEN_ARRAY:
      if (!parent->isanarray)
      {
         count = 0;
         for (temp=parent->children; temp != NULL; temp=temp->next)
            count++;

         if (count + 1 < tree->build_threshold)
         {
            child->next = parent->children;
            parent->children = child;
         }
         else {
            children = (stree_node<T>**)malloc(tree->alpha_size * sizeof(stree_node<T>*));
            if (children == NULL)
               return NULL;
            memset(children, 0, tree->alpha_size * sizeof(stree_node<T>*));

            for (temp=parent->children; temp != NULL; temp=temp->next)
            {
               children[(int)(*(temp->edgestr))] = temp;
            }

            parent->children = (stree_node<T>*) children;
            parent->isanarray = 1;

            children[(int) ch] = child;
         }
      }
      else
      {
         children = (stree_node<T>**) parent->children;
         children[(int) ch] = child;
      }
      break;

   case COMPLETE_ARRAY:
      children = (stree_node<T>**) parent->children;
      children[(int) ch] = child;
      break;
   }

   tree->idents_dirty = 1;

   return parent;
}

///*
//* int_stree_disc_from_parent
//*
//* Disconnect a node from its parent in the tree.
//* NOTE:  This procedure only does the link manipulation part of the
//*        disconnection process.  int_stree_disconnect is the real
//*        disconnection function.
//*
//* Parameters:  tree    -  A suffix tree
//*              parent  -  The parent node
//*              child   -  The child to be disconnected
//*
//* Return:  nothing.
//*/
template<typename T>
void int_stree_disc_from_parent(suffix_tree<T>* tree, stree_node<T>* parent, stree_node<T>* child)
{
   stree_node<T>* node;
   stree_node<T>* back;
   stree_node<T>** children;

   if (!parent->isanarray) {
      back = NULL;
      for (node=parent->children; node != child; node=node->next)
         back = node;

      if (back == NULL)
         parent->children = node->next;
      else
         back->next = node->next;
   }
   else {
      children = (stree_node<T>**) parent->children;
      children[(int)(*(child->edgestr))] = NULL;
   }
}

///*
//* int_stree_edge_merge
//*
//* When a node has no "leaves" and only one child, this function will
//* remove that node and merge the edges from parent to node and node
//* to child into a single edge from parent to child.
//*
//* Parameters:  tree  -  A suffix tree
//*              node  -  The tree node to be removed
//*
//* Return:  nothing.
//*/
template<typename T>
void int_stree_edge_merge(suffix_tree<T>* tree, stree_node<T>* node)
{
   int i, len;
   //stree_node* parent;
   stree_node<T>* child;
   stree_node<T>** children;

   if (node == stree_get_root(tree) || int_stree_isaleaf(tree, node) ||
      node->leaves != NULL)
      return;

   stree_node* parent = stree_get_parent(tree, node);
   if (!node->isanarray) {
      child = node->children;
      if (child == NULL || child->next != NULL)
         return;
   }
   else {
      child = NULL;
      children = (stree_node<T>**) node->children;
      for (i=0; i < tree->alpha_size; i++) {
         if (children[i] != NULL) {
            if (child != NULL)
               return;
            child = children[i];
         }
      }
      if (child == NULL)
         return;
   }
   len = stree_get_edgelen(tree, node);
   child->edgestr -= len;
   child->rawedgestr -= len;
   child->edgelen += len;

   int_stree_reconnect(tree, parent, node, child);
   tree->num_nodes--;
   tree->idents_dirty = 1;

   int_stree_free_node(tree, node);
}

///*
//* int_stree_disconnect
//*
//* Disconnects a node from its parent, and compacts the tree if that
//* parent is no longer needed.
//*
//* Parameters:  tree  -  a suffix tree
//*              node  -  a tree node
//*
//* Return:  The node at the end of the suffix line.
//*/
template<typename T>
void int_stree_disconnect(suffix_tree<T>* tree, stree_node<T>* node)
{
   int num;
   stree_node<T>* parent;

   if (node == stree_get_root(tree))
      return;

   parent = stree_get_parent(tree, node);
   int_stree_disc_from_parent(tree, parent, node);

   if (parent->leaves == NULL && parent != stree_get_root(tree) && 
      (num = stree_get_num_children(tree, parent)) < 2) {
         if (num == 0) {
            int_stree_disconnect(tree, parent);
            int_stree_delete_subtree(tree, parent);
         }
         else if (num == 1)
            int_stree_edge_merge(tree, parent);
   }

   tree->idents_dirty = 1;
}

///*
//* int_stree_edge_split
//*
//* Splits an edge of the suffix tree, and adds a new node between two
//* existing nodes at that split point.
//*
//* Parameters:  tree  -  a suffix tree
//*              node  -  The tree node just below the split.
//*              len   -  How far down node's edge label the split is.
//*
//* Return:  The new node added at the split.
//*/
template<typename T>
stree_node<T>* int_stree_edge_split(suffix_tree<T>* tree, stree_node<T>* node, int len)
{
   stree_node<T>* newnode;
   stree_node<T>* parent;

   if (node == stree_get_root(tree) ||
      len == 0 || stree_get_edgelen(tree, node) <= len)
      return NULL;

   newnode = int_stree_new_node(tree, node->edgestr, node->rawedgestr, len);
   if (newnode == NULL)
      return NULL;

   parent = stree_get_parent(tree, node);
   int_stree_reconnect(tree, parent, node, newnode);

   node->edgestr += len;
   node->rawedgestr += len;
   node->edgelen -= len;

   if (int_stree_connect(tree, newnode, node) == NULL)
   {
      node->edgestr -= len;
      node->rawedgestr -= len;
      node->edgelen += len;
      int_stree_reconnect(tree, parent, newnode, node);
      int_stree_free_node(newnode);
      return NULL;
   }

   tree->num_nodes++;
   tree->idents_dirty = 1;

   return newnode;
}

#pragma warning(disable: 4127)
///*
//* int_stree_set_idents
//*
//* Uses the non-recursive traversal to set the identifiers for the current
//* nodes of the suffix tree.  The nodes are numbered in a depth-first
//* manner, beginning from the root and taking the nodes in the order they
//* appear in the children lists.
//*
//* Parameters:  tree  -  A suffix tree
//*
//* Return:  nothing.
//*/
template<typename T>
void int_stree_set_idents(suffix_tree<T>* tree)
{
   enum { START, FIRST, MIDDLE, DONE, DONELEAF } state;
   int i, num, childnum, nextid;
   stree_node<T>* root;
   stree_node<T>* node;
   stree_node<T>* child;
   stree_node<T>** children;

   if (!tree->idents_dirty)
      return;

   /*
   * Use a non-recursive traversal where the `isaleaf' field of each node
   * is used as the value remembering the child currently being
   * traversed.
   */
   nextid = 0;
   node = root = stree_get_root(tree);
   state = START;
   while (1) {
      /*
      * The first time we get to a node.
      */
      if (state == START) {
         node->id = nextid++;

         num = stree_get_num_children(tree, node);
         if (num > 0)
            state = FIRST;
         else
            state = DONELEAF;
      }

      /*
      * Start or continue recursing on the children of the node.
      */
      if (state == FIRST || state == MIDDLE) {
         /*
         * Look for the next child to traverse down to.
         */
         if (state == FIRST)
            childnum = 0;
         else
            childnum = node->isaleaf;

         if (!node->isanarray) {
            child = node->children;
            for (i=0; child != NULL && i < childnum; i++)
               child = child->next;
         }
         else
         {
            children = (stree_node<T>**) node->children;
            for (i=childnum; i < tree->alpha_size; i++)
               if (children[i] != NULL)
                  break;
            child = (i < tree->alpha_size ? children[i] : NULL);
         }

         if (child == NULL)
            state = DONE;
         else {
            node->isaleaf = i + 1;
            node = child;
            state = START;
         }
      }

      /*
      * Last time we get to a node, do the post-processing and move back up,
      * unless we're at the root of the traversal, in which case we stop.
      */
      if (state == DONE || state == DONELEAF) {
         if (state == DONE)
            node->isaleaf = 0;

         if (node == root)
            break;

         node = node->parent;
         state = MIDDLE;
      }
   }

   tree->idents_dirty = 0;
}
#pragma warning(default: 4127)

#endif