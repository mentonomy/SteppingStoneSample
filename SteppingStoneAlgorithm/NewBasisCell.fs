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

module NewBasisCell


let find (cM: int Microsoft.FSharp.Math.Matrix) (u:int option array, v:int option array) =
    let (row, col, _) = 
        Microsoft.FSharp.Math.Matrix.Generic.foldi
            (fun row col ((state_row, state_col, state_value) as state) elem ->
                let value = elem - u.[row].Value - v.[col].Value
                if value < state_value then
                    (row, col, value)
                else
                    state) 
            (-1, -1, 0) cM
    if row >= 0 && col >= 0 then Some(row, col, 0) else None


