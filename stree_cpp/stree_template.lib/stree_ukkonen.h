#ifndef _STREE_UKKONEN_H_
#define _STREE_UKKONEN_H_

#include "stree_strmat.h"
#include "stree_strmat_get_funcs.h"

///*
//* stree_ukkonen_add_string
//*
//* Use Ukkonen's suffix tree construction algorithm to add a string
//* to a suffix tree.
//*
//* Parameters:  tree  -  a suffix tree
//*              S     -  the string to add
//*              Sraw  -  the raw version of the string
//*              M     -  the string length
//*              strid -  the string identifier
//*
//* Returns:  non-zero on success, zero on error.
//*/
template<typename T>
int stree_ukkonen_add_string(suffix_tree<T>* tree, const T *Str, const T *Sraw, int M, int strid)
{
   int i, j, g, h, gprime, edgelen, id;
   T *edgestr;
   stree_node<T>* node;
   stree_node<T>* lastnode;
   stree_node<T>* root;
   stree_node<T>* child;
   stree_node<T>* parent;
   stree_leaf<T>* leaf;

   id = int_stree_insert_string(tree, Str, Sraw, M, strid);
   if (id == -1)
      return 0;

   /*
   * Run Ukkonen's algorithm to add the string to the suffix tree.
   *
   * This implementation differs from the book description in 
   * several ways:
   *    1) The algorithm begins at the root of the tree and
   *       constructs I_{1} (the implicit suffix tree for just
   *       the first character) using the normal extension rules.
   *       The reason for this is to be able to handle generalized
   *       suffix trees, where that first character may already
   *       be in the tree.
   *    2) The algorithm inserts the complete suffix into the
   *       tree when extension rule 2 applies (rather than deal
   *       with the business of "increasing" suffices on the leaf
   *       nodes).
   *    3) All of the offsets into the sequence, and the phases of
   *       the algorithm, use the C array indices of 0 to M-1, not 1 to M.
   *    4) The algorithm handles the conversion from implicit tree
   *       to true suffix tree by adding an additional "phase" M.  In
   *       that phase, the leaves that normally would be added because
   *       of the end of string symbol '$' are added (without resorting
   *       to the use of a special symbol).
   *    5) The constructed suffix tree only has suffix links in
   *       the internal nodes of the tree (to save space).  However,
   *       the stree_get_suffix_link function will return the suffix links
   *       even for leaves (it computes the leaves' suffix links on the
   *       fly).
   */
   root = stree_get_root(tree);
   node = lastnode = root;
   g = 0;              // g is the number of characters along node's edge
   edgelen = 0;
   edgestr = NULL;

   for (i=0,j=0; i <= M; i++)  {
      for ( ; j <= i && j < M; j++) {
         /*
         * Perform the extension from S[j..i-1] to S[j..i].  One of the
         * following two cases holds:
         *    a) g == 0, node == root and i == j.
         *         (meaning that in the previous outer loop,
         *          all of the extensions S[1..i-1], S[2..i-1], ...,
         *          S[i-1..i-1] were done.)
         *    b) g > 0, node != root and the string S[j..i-1]
         *       ends at the g'th character of node's edge.
         */
         if (g == 0 || g == edgelen) {
            if (i < M) {
               /*
               * If an outgoing edge matches the next character, move down
               * that edge.
               */

               if ((child = stree_find_child(tree, node, Str[i])) != NULL)
               {
                  node = child;
                  g = 1;
                  edgestr = stree_get_edgestr(tree, node);
                  edgelen = stree_get_edgelen(tree, node);
                  break;
               }

               /*
               * Otherwise, add a new leaf out of the current node.
               */
               if ((leaf = int_stree_new_leaf(tree, id, i, j)) == NULL ||
                  (node = int_stree_connect(tree, node, (stree_node<T>*)leaf)) == NULL) {
                     if (leaf != NULL)
                        int_stree_free_leaf(leaf);
                     return 0;
               }

               tree->num_nodes++;
            }
            else {
               /*
               * If i == M, then the suffix ends inside the tree, so
               * add a new intleaf at the current node.
               */
               if (node->isaleaf && (node = int_stree_convert_leafnode(tree, node)) == NULL)
                  return 0;

               if (!int_stree_add_intleaf(node, id, j))
                  return 0;
            }

            if (lastnode != root && lastnode->suffix_link == NULL)
               lastnode->suffix_link = node;
            lastnode = node;
         }
         else {
            /*
            * g > 0 && g < edgelen, and so S[j..i-1] ends in the middle
            * of some edge.
            *
            * If the next character in the edge label matches the next
            * input character, keep moving down that edge.  Otherwise,
            * split the edge at that point and add a new leaf for the
            * suffix.
            */
            if (i < M && Str[i] == edgestr[g]) {
               g++;
               break;
            }

            if ((node = int_stree_edge_split(tree, node, g)) == NULL)
               return 0;

            edgestr = stree_get_edgestr(tree, node);
            edgelen = stree_get_edgelen(tree, node);

            if (i < M) {
               if ((leaf = int_stree_new_leaf(tree, id, i, j)) == NULL ||
                  (node = int_stree_connect(tree, node, (stree_node<T>*) leaf)) == NULL)
               {
                     if (leaf != NULL)
                        int_stree_free_leaf(leaf);
                     return 0;
               }

               tree->num_nodes++;
            }
            else {
               /*
               * If i == M, then the suffix ends inside the tree, so
               * add a new intleaf at the node created by the edge split.
               */
               if (node->isaleaf && (node = int_stree_convert_leafnode(tree, node)) == NULL)
                  return 0;

               if (!int_stree_add_intleaf(node, id, j))
                  return 0;
            }

            if (lastnode != root && lastnode->suffix_link == NULL)
               lastnode->suffix_link = node;
            lastnode = node;
         }

         /* Now, having extended S[j..i-1] to S[j..i] by rule 2, find where
         * S[j+1..i-1] is.  Note that the values of node and g have not
         * changed in the above code (since stree_edge_split splits the
         * node on the g'th character), so either g == 0 and node == root
         * or the string S[j..i-1] ends at the g-1'th character of node's
         * edge (and node is not the root).
         */
         if (node == root)
            ;
         else if (g == edgelen && node->suffix_link != NULL) {
            node = node->suffix_link;

            edgestr = stree_get_edgestr(tree, node);
            edgelen = stree_get_edgelen(tree, node);

            g = edgelen;
            continue;
         }
         else {
            /*
            * Move across the suffix link of the parent (unless the
            * parent is the root).
            */
            parent = stree_get_parent(tree, node);
            if (parent != root)
            {
               node = parent->suffix_link;

            }
            else {
               node = root;
               g--;
            }
            edgelen = stree_get_edgelen(tree, node);

            /*
            * Use the skip/count trick to move g characters down the tree.
            */
            h = i - g;
            while (g > 0)
            {
               node = stree_find_child(tree, node, Str[h]);


               gprime = stree_get_edgelen(tree, node);
               if (gprime > g)
                  break;

               g -= gprime;
               h += gprime;
            }

            edgelen = stree_get_edgelen(tree, node);
            edgestr = stree_get_edgestr(tree, node);

            /*
            * After the walk down, either "g > 0" and S[j+1..i-1] ends g
            * characters down the edge to `node', or "g == 0" and S[j+1..i-1]
            * really ends at `node' (i.e., all of the characters on the edge
            * label to `node' match the end of S[j+1..i-1]).
            *
            * If "g > 0" or "g == 0" but `node' points to a leaf (which could
            * happen if S[j+1..i-1] was the suffix of a previously added
            * string), then we must delay the setting of the suffix link
            * until a node has been created.  (With the suffix tree data 
            * structure, no suffix links can safely point to leaves of the
            * tree because a leaf may be converted into a node at some future
            * time.)
            */
            if (g == 0)
            {
               if (lastnode != root && !node->isaleaf &&
                  lastnode->suffix_link == NULL) {
                     lastnode->suffix_link = node;
                     lastnode = node;
               }

               if (node != root)
                  g = edgelen;
            }
         }
      }
   }

   return 1;
}

#endif // _STREE_UKKONEN_H_