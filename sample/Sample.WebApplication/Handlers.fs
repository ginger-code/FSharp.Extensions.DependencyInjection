module Sample.WebApplication.Handlers

open Giraffe

open Sample.Domain.Wrappers

let addOne (i: int) : HttpHandler =
    fun next ctx ->
        let (AddOne addOne) = ctx.GetService()
        let result = addOne i
        text $"{i} + 1 = {result}" next ctx

let subOne (i: int) : HttpHandler =
    fun next ctx ->
        let (SubOne subOne) = ctx.GetService()
        let result = subOne i
        text $"{i} - 1 = {result}" next ctx
