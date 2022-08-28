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

module SteppingStone

let private makeDict keySelector valSelector lst =
    let key (r, c, _) = keySelector (r, c)
    let value (r, c, v) = (valSelector (r, c), v)
    lst
    |> Seq.groupBy key
    |> Seq.map (fun (key, valSeq) -> (key, valSeq |> Seq.map value))
    |> dict


let find_path ((targetRow, targetCol, _) as target) lstSolution =

    (*  Create the lookup tables for rows and columns.
        Both use makeDict but pass in the 'selector'
        functions in different order    *)
    let rowDict = lstSolution |> makeDict fst snd
    let colDict = lstSolution |> makeDict snd fst

    (*  From current position, find the row we're in,
        then go through all cells (except our current one).
        If we end up on the same column as the entering
        cell, aka 'target', then this is the last element
        of the list we are creating. Otherwise, check to
        see if we can move on in this column. If both fails,
        there's nothing there there.    *)
    let rec findInRow currentRow currentCol =
        rowDict.[currentRow] 
        |> Seq.tryPick (fun (col, v) ->
            if col=currentCol then None
            else if col=targetCol then Some([(currentRow, col, v)])
            else
                match findInCol currentRow col with
                | Some(l) -> Some((currentRow, col, v)::l)
                | _ -> None)

    (*  Like findInRow, but looking through current row *)
    and findInCol currentRow currentCol =
        colDict.[currentCol] 
        |> Seq.tryPick (fun (row, v) ->
            if row=currentRow then None
            else if row=targetRow then Some([(row, currentCol, v)])
            else
                match findInRow row currentCol with
                | Some(l) -> Some((row, currentCol, v)::l)
                | _ -> None)

    (*  Return value is created by calling one of the
        nested functions.   *)
    target::(findInCol targetRow targetCol).Value

