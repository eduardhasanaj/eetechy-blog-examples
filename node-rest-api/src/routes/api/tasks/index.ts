import { FastifyPluginAsync } from "fastify"
import Task from "../../../models/task";
import TaskService from "../../../services/task_service";
import { ListQueryOptions, ListQueryOptionsSchema } from "../../../types/list_query_options";
import  { PathIdParam, PathIdParamSchema } from "../../../types/path_id_param";
import { RequiredTaskSchema } from "../../../schemas/task_schema";

const taskRouter: FastifyPluginAsync = async (fastify, opts): Promise<void> => {

  fastify.post<{Body: Task, Reply: Task}>(
    '/',
    {
      schema: {
        body: RequiredTaskSchema
      }, 
    },
    async (req, res) => {
      try {
        return TaskService.createTask(req.body)
      }
      catch(err: any) {
        return err;
      }
    }
  )

  fastify.get<{Querystring: ListQueryOptions, Reply: Task[]}>(
    '/',
    {
      schema: {
        querystring: ListQueryOptionsSchema,
      },
    },
    async (req, res) => {
      return await TaskService.getTaskList(req.query)
    }
    
  )
  
  /**
   * Get task by id
   */
  fastify.get<{ Params: PathIdParam, Reply: Task | Error}>(
    '/:id',
    {
      schema: {
        params: PathIdParamSchema,
      }
    },
    async (req, res) => {
      try {
        return await TaskService.getTask(req.params.id);
      }
      catch(err: any) {
        return err;
      }
    }
  )

  fastify.put<{Params: PathIdParam, Body: Task, Reply: Task | Error}>(
    '/:id',
    {
      schema: {
        params: PathIdParamSchema,
        body: RequiredTaskSchema
      },
    },
    async (req, res) => {
      req.body.id = req.params.id;
      try {
        return await TaskService.updateTask(req.body)
      }
      catch(err: any) {
        return err
      }
    }
  )

  fastify.delete<{Params: PathIdParam, Reply: Task | Error}>(
    '/:id',
    {
      schema: {
        params: PathIdParamSchema,
      },
    },
    async (req, res) => {
      try {
        return await TaskService.deleteTask(req.params.id)
      }
      catch(err: any) {
        return err
      }
    }
  )
}

export default taskRouter;
