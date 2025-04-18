import axios from "axios";
import {API_URL} from '../common/ApiSettings';

function useDishesRepository() {
    const getDishes = async () => {
        try {
            const response = await axios.get(API_URL + '/api/dishes');

            return response.data;
        } catch (e) {
            console.log(e);
        }
    };

    return {getDishes};
}

export default useDishesRepository;