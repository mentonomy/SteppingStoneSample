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
module Solver

type Solver(costMatrix: int Microsoft.FSharp.Math.Matrix,  // Matrix class no longer available, needs fixing
            srcCapacity: int list,
            dstCapacity: int list,
            ?FindFeasible) = // Optional: function to determine initial solution

    (*  To start with, we check if the data makes sense. If the problem
        is not balanced, or if the dimensions of the cost matrix are wrong,
        there's no point in starting the algorithm.     *)
    do if costMatrix.Dimensions <> (List.length srcCapacity, List.length dstCapacity)
        || List.sum srcCapacity <> List.sum dstCapacity then
        failwith "Invalid Data."

    (*  If no function is specified for finding the initial solution,
        we use the North-West Corner function by default    *)
    let InitialSolutionFinder = match FindFeasible with
                                | Some(fn) -> fn
                                | _ -> InitialFeasibleSolution.find_feasible_nwc

    (*  Create a sequence that gives us one intermediate solution at
        a time. The last element of the sequence is the optimal
        solution                *)
    member s.Steps =
        let rec steps sol =
            seq {
                // current solution = sequence element
                yield sol 
                // find dual values for current solution
                let duals = Dual.calc_dual_values costMatrix sol
                // for dual values, find new entering cell
                match NewBasisCell.find costMatrix duals with
                | Some(new_cell) ->
                    // use stepping stone to find path for new cell
                    let new_path = SteppingStone.find_path new_cell sol
                    // adjust values for path
                    let new_path_adjusted = NewSolution.new_path new_path
                    // from current solution and new cell's path,
                    // make new solution
                    let transformed_solution = NewSolution.transform_basis_solution sol new_path_adjusted 
                    // do it all again, with the new solution
                    yield! steps transformed_solution
                // if there's no entering cell, we're done
                | _ -> ()
            }
        // Initialisation: solution found with the specified method
        InitialSolutionFinder costMatrix srcCapacity dstCapacity |> steps

    (*  Go directly to last solution, which is the optimal one 
        -- See http://msdn.microsoft.com/en-us/library/system.linq.enumerable_methods.aspx
        -- for more .NET extension methods that can be used on F# sequences  *)
    member s.Solution =
        System.Linq.Enumerable.Last s.Steps

    (*  Calculate total costs for a given solution  
        -- See http://stackoverflow.com/questions/3912821/f-instance-syntax/3914236#3914236
        -- for use of __.       *)
    member __.Cost(solution: (int*int*int) list) =
        solution |> List.fold (fun acc (r, c, v) -> acc + costMatrix.[r, c] * v) 0
        

