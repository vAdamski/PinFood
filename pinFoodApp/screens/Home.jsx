import React, {useEffect, useState} from 'react';
import {View} from "react-native";
import axios from "axios";

function Home() {
    const [user, setUser] = useState(null);

    useEffect(() => {
        const testCall = async () => {
            const response = axios.get('https://localhost:5001/api/users/d79b6c0c-ca05-44d3-97ed-d953d8945216')
                .then((response) => {
                    console.log(response.data);
                })
                .catch((error) => {
                    console.error(error);
                });

            console.log(response);

            setUser(response.data.id);
        }

        testCall();
    }, []);

    return (
        <View>
            <Text>Home</Text>
            <Text>Welcome to the Home Screen!</Text>
            <Text>User ID: {user}</Text>
        </View>
    );
}

export default Home;