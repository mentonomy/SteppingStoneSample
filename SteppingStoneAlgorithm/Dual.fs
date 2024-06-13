(*
   The MIT License

   Copyright (c)2011 Alexander Rautenberg

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
module Dual


let calc_dual_values (cM: int Microsoft.FSharp.Math.Matrix) basis_solution = // Matrix class no longer available, needs fixing
    let rows, cols = cM.Dimensions
    (*  Create arrays to hold the dual values for
        sources (rows) and destinations (columns).
        To be able to express unknown quantities,
        we use arrays of option type.   *)
    let u = Array.create rows None
    let v = Array.create cols None
    (*  Here we go through all the cells of the
        current solution to see for which we
        can calculate the missing dual value.
        We can only calculate v if we have the
        corresponding u, or viceversa. 
        Any cells for which we don't have
        u or v are returned in a new list.  *)
    let rec calc sol =
        match sol with
        | [] -> [] // Not necessary, but avoids compiler warnings
        | (row, col, _) as h::t ->
            // Get the first cell
            match u.[row], v.[col] with
            // No u or v, return head and try tail
            | None, None -> h::calc t
            // u and v known. Can't really happen,
            // but again avoids compiler warnings
            | Some(ui), Some(vi) -> calc t
            // u known: calculate v, drop head, process tail
            | Some(ui), None ->
                v.[col] <- Some(cM.[row, col] - ui)
                calc t
            // v known: calculate u, drop head, process tail
            | None, Some(vi) ->
                u.[row] <- Some(cM.[row, col] - vi)
                calc t
    (*  This implements a loop that is executed until
        all u and v are known   *)
    let rec ccalc sol =
        match calc sol with // do calculations
        | [] -> ()  // all processed -> done
        | l -> ccalc l // some cells left -> try those again
    (*  Initialise an arbitrary element of one array with 0 *)
    u.[0] <- Some(0)
    (*  Start calculations *)
    ccalc basis_solution
    (*  Return two arrays with dual values.
        The arrays contain option type values; we could
        strip them off before returning them, but here we
        just let the next step of the algorithm take this
        into account and return the arrays as they are. *)
    u, v





