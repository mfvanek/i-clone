#ifndef _STREE_STRMAT_STRUCTS_H_
#define _STREE_STRMAT_STRUCTS_H_

#include <string.h>

#define LINKED_LIST 0
#define SORTED_LIST 1
#define LIST_THEN_ARRAY 2
#define COMPLETE_ARRAY 3

struct stree_intleaf
{
   int strid;
   int pos;
   stree_intleaf* next;

   void init()
   {
      memset(this, 0, sizeof(stree_intleaf));
   }
};

//template<typename T>
//void memset(stree_intleaf*, int, T) {class CompilerError; CompilerError do_not_use_memset_for_this_struct }

template<typename T>
struct stree_node
{
   int isaleaf;
   int id;

   T *edgestr;
   T *rawedgestr;
   int edgelen;

   stree_node* parent;
   stree_node* next;

   stree_node* suffix_link;

   int isanarray;
   stree_node* children;

   stree_intleaf* leaves;

   void init()
   {
      memset(this, 0, sizeof(stree_node));
   }
};

template<typename T>
struct stree_leaf
{
   int isaleaf;
   int id;

   T *edgestr;
   T *rawedgestr;
   int edgelen;

   stree_node<T>* parent;
   stree_node<T>* next;

   int strid;
   int pos;

   void init()
   {
      memset(this, 0, sizeof(stree_leaf));
   }
};

//template<typename T>
//void memset(stree_leaf*, int, T) {class CompilerError; CompilerError do_not_use_memset_for_this_struct }

template<typename T>
struct suffix_tree
{
   stree_node<T>* root;
   int num_nodes;

   T **strings;
   T **rawstrings;
   int *lengths;
   int *ids;
   int nextslot;
   int strsize;
   int copyflag;

   int alpha_size;
   int build_type;
   int build_threshold;
   int idents_dirty;
};

const int MAX_ALPHABET_SIZE = 128;

#define stree_get_next(tree, node) (node)->next
#define stree_get_root(tree) ((tree)->root)
#define stree_get_num_nodes(tree) ((tree)->num_nodes)
#define stree_get_parent(tree,node) ((node)->parent)
#define stree_get_edgestr(tree,node)  ((node)->edgestr)
#define stree_get_rawedgestr(tree,node)  ((node)->rawedgestr)
#define stree_get_edgelen(tree,node)  ((node)->edgelen)

#define stree_get_ident(tree,node) \
   (!((tree)->idents_dirty) ? (node)->id \
   : (int_stree_set_idents(tree), (node)->id))

#define int_stree_has_intleaves(tree,node) \
   (int_stree_isaleaf(tree,node) ? 0 : ((node)->leaves != NULL))
#define int_stree_get_intleaves(tree,node) \
   ((node)->isaleaf ? NULL : (node)->leaves)

#define int_stree_get_leafpos(tree,node)  ((node)->pos)
#define int_stree_get_string(tree,id)  ((tree)->strings[(id)])
#define int_stree_get_rawstring(tree,id)  ((tree)->rawstrings[(id)])
#define int_stree_get_length(tree,id)  ((tree)->lengths[(id)])
#define int_stree_get_strid(tree,id)  ((tree)->ids[(id)])

const int OPT_NODE_SIZE = 24;
const int OPT_LEAF_SIZE = 12;
const int OPT_INTLEAF_SIZE = 12;

#endif // _STREE_STRMAT_STRUCTS_H_