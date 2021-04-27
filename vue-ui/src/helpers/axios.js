import axios from 'axios';

/**
 * Cấu hình mặc định cho axios.
 * CreatedBy: dbhuan (27/04/2021)
 */
export default axios.create({
    baseURL: "https://localhost:44378",
    timeout: 1000,
    headers: {
        'Content-Type': 'application/json'
    }
});