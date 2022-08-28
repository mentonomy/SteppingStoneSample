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
module NewSolution


let rec private find_min path = 
    match path with
    | [_;(_, _, v)] -> v
    | _::(_, _, v)::t -> min v (find_min t)
    | _ -> failwith "Unexpected data for find_min."


let new_path path =
    let m = find_min path
    let rec flatten p =
        match p with
        | (r1, c1, v1)::(r2, c2, v2)::t -> 
            (r1, c1, v1 + m)::(r2, c2, v2 - m)::flatten t
        | _ -> []
    flatten path    

let rec private remove_first pred lst =
    match lst with
    | h::t when pred h -> t
    | h::t -> h::remove_first pred t
    | _ -> []

let rec private remove_first_zero_t =
    function    | (h::t) -> h::remove_first (fun (_, _, v) -> v=0) t 
                | _ -> []

    

let transform_basis_solution solution new_path =
    let change_set = 
        new_path |> Seq.map (fun (r, c, _) -> (r, c)) |> Set.ofSeq
    let nonzero_new_path =
        new_path |> remove_first_zero_t
    solution 
    |> List.filter (fun (r, c, _) -> not (change_set.Contains(r, c)))
    |> List.append nonzero_new_path




