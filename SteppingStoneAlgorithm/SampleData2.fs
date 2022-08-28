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
module SampleData2

let srcCapacity = [ 8
                    6
                    12
                    8
                    10
                    12
                    6
                    8
                    4
                    8
                    14
                    8
                    11
                    9
                    11
                    12
                    5
                    13
                    2 ]

let dstCapacity = [15;12;20;12;16;14;18;16;20;24]


let costMatrix = Microsoft.FSharp.Math.Matrix.Generic.ofList
                    [   [80;100;64;42;24;94;3;85;42;100]
                        [80;22;78;12;10;60;94;34;44;10]
                        [14;87;10;81;92;95;5;99;34;47]
                        [91;53;50;65;45;43;82;73;6;52]
                        [34;13;3;85;35;91;72;19;47;98]
                        [61;6;74;91;39;60;68;87;90;84]
                        [6;44;20;59;86;84;58;60;51;41]
                        [77;7;84;75;6;27;93;45;80;39]
                        [29;36;75;37;88;57;53;12;65;33]
                        [36;7;16;72;53;29;86;83;75;31]
                        [81;100;5;37;58;13;44;82;72;34]
                        [57;100;65;56;72;63;14;13;37;7]
                        [19;30;58;32;66;47;68;20;30;16]
                        [17;61;67;58;55;28;42;59;95;91]
                        [58;29;22;12;31;69;42;8;60;74]
                        [26;28;63;47;56;29;8;10;21;98]
                        [17;17;74;1;66;47;91;15;72;71]
                        [45;13;82;92;46;62;50;29;47;16]
                        [24;64;75;40;29;8;80;67;67;16]
                        ]

