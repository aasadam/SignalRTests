import axios from 'axios';
import functions from '../functions'

//const baseURL = window.location.origin + "/api/";
const baseURL = "https://localhost:5001/api/";

let instance = axios.create({
    baseURL,
    headers: {
        'Content-Type': 'application/json',
        'Accept': 'application/json',
    }
});

instance.defaults.headers.get['Pragma'] = 'no-cache';
instance.defaults.headers.get['Cache-Control'] = 'no-cache, no-store';

instance.interceptors.request.use(function (config) {
    const USER_TOKEN = functions.GetCookie('TOKEN');
    config.headers.Authorization = USER_TOKEN ? `Bearer ${USER_TOKEN}` : null;
    return config;
});

export default instance