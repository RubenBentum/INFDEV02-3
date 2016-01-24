﻿module StateTraceSamples

open CommonLatex
open SlideDefinition
open CodeDefinition

let slides = 
  [
    CSharpStateTrace(TextSize.FootnoteSize,
          ((interfaceDef "ICounter" 
              [
                typedSig "Incr" ["int","diff"] "void"
              ]) >>
           ((classDef "Counter" 
              [
                implements "ICounter"
                typedDecl "cnt" "int"
                typedDef "Counter" [] "" ("this.cnt" := constInt 0)
                typedDef "Incr" ["int","diff"] "void" ("this.cnt" := (var "this.cnt" .+ var "diff"))
              ]) >>
            (((typedDeclAndInit "c" "ICounter" (newC "Counter" [ConstInt 5])) >>
               (methodCall "c" "Incr" [ConstInt 5])) >> 
                endProgram))),
          { Stack = [["PC", constInt 1] |> Map.ofList]; Heap = Map.empty; InputStream = []; HeapSize = 1 })

    CSharpStateTrace(TextSize.FootnoteSize,
          ((classDef "Counter" 
              [
                typedDecl "cnt" "int"
                typedDef "Counter" [] "" ("this.cnt" := constInt 0)
                typedDef "Incr" ["int","diff"] "void" ("this.cnt" := (var "this.cnt" .+ var "diff"))
              ]) >>
           (((typedDeclAndInit "c" "Counter" (newC "Counter" [ConstInt 5])) >>
              (methodCall "c" "Incr" [ConstInt 5])) >> 
               endProgram)),
          { Stack = [["PC", constInt 1] |> Map.ofList]; Heap = Map.empty; InputStream = []; HeapSize = 1 })

    PythonStateTrace(TextSize.FootnoteSize,
          ((classDef "Counter" 
              [
                def "__init__" ["self"] ("self.cnt" := constInt 0)
                def "incr" ["self"; "diff"] ("self.cnt" := (var "self.cnt" .+ var "diff"))
              ]) >>
            ((("c" := newC "Counter" []) >>
              (methodCall "c" "incr" [ConstInt 5])) >>
              endProgram)),
          { Stack = [["PC", constInt 1] |> Map.ofList]; Heap = Map.empty; InputStream = []; HeapSize = 1 })

    CSharpCodeBlock(TextSize.FootnoteSize,
        (classDef "Counter" 
          [
            typedDecl "cnt" "int"
            typedDef "Counter" [] "" ("cnt" := constInt 0)
            typedDef "Incr" ["int","diff"] "void" ("this.cnt" := (var "this.cnt" .+ var "diff"))
          ]) >>
        (((typedDeclAndInit "c" "Counter" (newC "Counter" [ConstInt 5])) >>
          (methodCall "c" "incr" []))))


    PythonStateTrace(TextSize.Small,
      (def "f" ["x"] 
        (ifelse (var "x" .> constInt 0) 
          (ret ((call "f" [constInt -20]) .+ constInt 1))
          (ret (var "x" .* constInt 2))) >>
       call "f" [constInt 20] >>
       endProgram),
      { Stack = [["PC", constInt 1] |> Map.ofList]; Heap = Map.empty; InputStream = []; HeapSize = 1 }
    )
  ]