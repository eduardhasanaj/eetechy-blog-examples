import { Type } from "@sinclair/typebox";

export const RequiredTaskSchema = Type.Object({
    id: Type.Optional(Type.Integer()),
    title: Type.String(),
    description: Type.String(),
    status: Type.String(),
    start_time: Type.Optional(Type.String()),
    end_time: Type.Optional(Type.String()),

    deleted: Type.Optional(Type.Boolean())
});

