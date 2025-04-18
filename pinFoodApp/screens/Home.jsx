import {View} from "react-native";
import { RecipeSwiper } from './RecipeSwiper';

function Home() {
    return (
        <View style={{ flex: 1 }}>
            <RecipeSwiper />
        </View>
    );
}

export default Home;