import styled from "styled-components"

const ListTypeEvent = styled.div`
    display: flex;
    margin-top: 1rem;
    justify-content: space-between;
    flex-wrap: wrap;
    column-gap: 2rem;
    row-gap: 0.5rem;
    align-items: center;
    padding-left: 1rem;
    padding-right: 1rem;
`

const SContainerScrollable = styled.div`
    background-color: transparent;
    border-radius: 20px;
    max-height: 330px;
    overflow-y: auto;
    scrollbar-width: thin;
    ::-webkit-scrollbar {
        width: 10px;
    }
    ::-webkit-scrollbar-track{
        margin-top: 40px;
    }

    ::-webkit-scrollbar-thumb {
        background: rgb(180, 180, 180); 
        border-radius: 10px;
    }
`

const TypeEventTarget = styled.div`
    min-width: 60px;
    text-align: left;
    display: flex;
    column-gap: 0.5rem;
    align-items: center;
    align-items: stretch;
`

export { ListTypeEvent, SContainerScrollable, TypeEventTarget }