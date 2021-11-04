import Knex = require('knex')
import { Model } from 'objection';
import config from '../config'
import fp from 'fastify-plugin'

export interface DBConfig {
    
}

export default fp<DBConfig>(async (fastify, opts) => {
    const knex = Knex(config.development.database)
    Model.knex(knex);

    await checkHeartbeat(knex);
    
    fastify.decorate('knex', knex)
});

async function checkHeartbeat(knex: Knex<any,unknown[]>) {
    await knex.raw('SELECT 1')
}