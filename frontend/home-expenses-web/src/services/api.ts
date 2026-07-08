import axios from "axios";

const api = axios.create({
  baseURL: "http://localhost:5045/api"
});

export default api;