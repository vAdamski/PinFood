import {Button, View} from 'react-native';
import {AuthProvider, useAuth} from "./app/context/AuthContext";
import {NavigationContainer} from "@react-navigation/native";
import {SafeAreaView} from "react-native-safe-area-context";
import {createNativeStackNavigator} from "@react-navigation/native-stack";

import Login from './screens/Login';
import Home from './screens/Home';
import Favorites from './screens/Favorites';
import {createBottomTabNavigator} from "@react-navigation/bottom-tabs";
import Profile from "./screens/Profile";

const Stack = createNativeStackNavigator();
const Tab = createBottomTabNavigator();

export default function App() {
    return (
        <AuthProvider>
            <Layout></Layout>
        </AuthProvider>
    );
}

const MainTabs = () => (
    <Tab.Navigator screenOptions={{ headerShown: false }}>
        <Tab.Screen name="Główna" component={Home} />
        <Tab.Screen name="Polubione" component={Favorites} />
        <Tab.Screen name="Profil" component={Profile}/>
    </Tab.Navigator>
);

export const Layout = () => {
    const { authState } = useAuth();

    return (
        <NavigationContainer>
            <Stack.Navigator screenOptions={{ headerShown: false }}>
                {authState.authenticated ? (
                    <Stack.Screen name="Main">
                        {() => <MainTabs/>}
                    </Stack.Screen>
                ) : (
                    <Stack.Screen name="Login" component={Login} />
                )}
            </Stack.Navigator>
        </NavigationContainer>
    );
};
