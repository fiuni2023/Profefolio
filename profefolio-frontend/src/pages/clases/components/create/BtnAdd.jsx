import React from 'react'
import { BsFillPlusCircleFill } from 'react-icons/bs'

const BtnAdd = ({handleShowModal = () => {}}) => {
    console.log(handleShowModal)
    return <>
        <div className='NButtonForSideA'>
            <div className="buttonNavBarAa">
                <button className="buttonNavBarA" onClick={handleShowModal}>
                    <BsFillPlusCircleFill />
                </button>
            </div>
        </div>
    </>
}

export default BtnAdd