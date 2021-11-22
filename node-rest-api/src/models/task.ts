import { Model } from 'objection'

export default class Task extends Model {
    id!: number
    title!: string
    description!: string
    status!: string
    startTime!: Date
    endTime!: Date
    deleted!: boolean
    
    static tableName = 'tasks'
}