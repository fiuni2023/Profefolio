import { useEffect, useState } from "react";

export const useFetchEffect = ( asyncFunc = async() => {}, dependences = [], settings = {condition:() => {return true}, handleSuccess:()=>{}, handleError: ()=>{}} ) => {
    const [loading, setLoading] = useState(false)
    const [error, setError] = useState(false)
    const [toggle, toggleFetch] = useState([])

    const doFetch = () => {
        let newFetch = toggle
        newFetch = [...newFetch, {}]
        toggleFetch(newFetch)
    }

    useEffect(()=>{
        setLoading(true)
        if(settings.condition){
            asyncFunc()
            .then((result)=>{
                setLoading(false)
                settings.handleSuccess(result)
            })
            .catch((error)=>{
                setError(true)
                settings.handleError(error)
            })
        }
    }, [ dependences, settings, asyncFunc])

    return [loading, error, doFetch]
}