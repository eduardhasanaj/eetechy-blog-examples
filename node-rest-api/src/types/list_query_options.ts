import { Type } from '@sinclair/typebox'

export interface ListQueryOptions {
    page: number
    count: number
    query: string
}

export const ListQueryOptionsSchema = Type.Object({
    page: Type.Integer(),
    count: Type.Integer(),
    query: Type.Optional(Type.String())
})