
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

module Program


let srcCapacity = SampleData1.srcCapacity
let dstCapacity = SampleData1.dstCapacity
let costMatrix = SampleData1.costMatrix


let solver = Solver.Solver(costMatrix, srcCapacity, dstCapacity)

// Go through all the solutions in the sequence and
// observe the total cost going down. costs has these
// numbers in a list to print or look at in the debugger.
let costs = solver.Steps |> Seq.mapi (fun i s -> (i, solver.Cost s)) |> Seq.toList





