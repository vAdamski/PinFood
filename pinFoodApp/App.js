import {Button} from 'react-native';
import {AuthProvider, useAuth} from "./app/context/AuthContext";
import {NavigationContainer} from "@react-navigation/native";
import {createStackNavigator} from "@react-navigation/native/src/__stubs__/createStackNavigator";
import Home from "./screens/Home";
import Login from "./screens/Login";

const Stack = createStackNavigator();

export default function App() {
    return (
        <AuthProvider>
            <Layout></Layout>
        </AuthProvider>
    );
}

export const Layout = () => {
    const {authState, onLogout} = useAuth();


    return (
        <NavigationContainer>
            <Stack.Navigator id={"mainStack"}>
                {authState.authenticated ? (
                    <Stack.Screen name="Home" component={Home} options={{
                        headerRight: () => <Button onPress={onLogout} title="Wyloguj"/>
                    }}></Stack.Screen>
                ) : (
                    <Stack.Screen name="Login" component={Login}></Stack.Screen>
                )}
            </Stack.Navigator>
        </NavigationContainer>
    );
}
