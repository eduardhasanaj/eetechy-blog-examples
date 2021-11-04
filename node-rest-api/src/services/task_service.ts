import Task from "../models/task";
import { ListQueryOptions } from "../types/list_query_options";
import httpErrors = require("http-errors");

export default class TaskService {
    static async createTask(task: Task): Promise<Task> {
        return await Task.query().insert(task);
    }

    static async updateTask(task: Task): Promise<Task> {
        const oldTask = await this.getTask(task.id);

        return await oldTask.$query().updateAndFetch(task);
    }

    static async deleteTask(id: number): Promise<Task> {
        const t = await this.getTask(id);

        await t.$query().updateAndFetch({
            deleted: true
        });

        return t;
    }

    static async getTaskList(lso: ListQueryOptions): Promise<Task[]> {
        const offset = lso.count * (lso.page - 1)
        return await Task.query()
            .where('deleted', false)
            .limit(lso.count)
            .offset(offset);
    }

    static async getTask(id: number): Promise<Task> {
        const task = await Task.query()
            .findById(id)
            .where('deleted', false);

        if (!task) {
            throw new httpErrors.NotFound()
        }

        return task;
    }
}