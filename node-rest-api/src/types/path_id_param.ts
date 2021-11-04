import { Type } from "@sinclair/typebox";

export interface PathIdParam {
    id: number
}

export const PathIdParamSchema = Type.Object({
    id: Type.Integer()
})