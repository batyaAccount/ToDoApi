import axios from 'axios';

// debugger
axios.defaults.baseURL = process.env.REACT_APP_API_KEY;

axios.interceptors.response.use(
  response => response,
  error => {
    console.error('Error in response:', error);
    return Promise.reject(error);
  }
);

export default {
  getTasks: async () => {
    try {
      console.log(process.env.REACT_APP_API_KEY+'/items');
      
      const result = await axios.get(`/items`)
      return result.data;

    } catch (e) {
      console.log(e);
      
      alert('Error fetching data')
    }
  },

  addTask: async (name) => {
    console.log('addTask', name)
    try {
      const result = await axios.post(`/items`, { name: name, isComplete: false })
    } catch (e) {
      alert('Error adding')
    }
    return {};

  },

  setCompleted: async (id, isComplete) => {
    console.log('setCompleted', { id, isComplete })
    try {
      const result = await axios.put(`/items/${id}`, { isComplete: isComplete })
    } catch (e) {
      alert('Error updating item')
    }
    return {};
  },

  deleteTask: async (id) => {
    console.log('deleteTask')
    try {
      const result = await axios.delete(`/items/${id}`)
    } catch (e) {
      alert('Error deleting item')
    }
  }
};
