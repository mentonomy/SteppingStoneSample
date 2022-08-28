(*
   The MIT License

   Copyright (c)2011 Fondevila Ltd

   Permission is hereby granted, free of charge, to any person obtaining a copy
   of this software and associated documentation files (the "Software"), to deal
   in the Software without restriction, including without limitation the rights
   to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
   copies of the Software, and to permit persons to whom the Software is
   furnished to do so, subject to the following conditions:

   The above copyright notice and this permission notice shall be included in
   all copies or substantial portions of the Software.

   THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
   IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
   FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
   AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
   LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
   OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
   THE SOFTWARE.
*)
module InitialFeasibleSolution


(*  North-West Corner Method    *)
let find_feasible_nwc costMatrix src dst =
    let rec find_feasible src_l src_idx dst_l dst_idx =
        match src_l, dst_l with
        | h_src::t_src, h_dst::t_dst when h_src < h_dst ->
            (src_idx, dst_idx, h_src)::find_feasible 
                t_src (src_idx + 1) ((h_dst - h_src)::t_dst) dst_idx
        | h_src::t_src, h_dst::t_dst ->
            (src_idx, dst_idx, h_dst)::find_feasible 
                ((h_src - h_dst)::t_src) src_idx t_dst (dst_idx + 1)
        | _ -> []
    find_feasible src 0 dst 0



