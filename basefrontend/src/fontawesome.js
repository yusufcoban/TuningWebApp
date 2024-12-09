// src/fontawesome.js
import { library } from '@fortawesome/fontawesome-svg-core'
import { FontAwesomeIcon } from '@fortawesome/vue-fontawesome'
import { faUser, faLock, faHouse, faSignInAlt, faPlus, faTrashCan, faRightFromBracket} from '@fortawesome/free-solid-svg-icons'

// Add icons to the library
library.add(faUser, faLock, faSignInAlt, faHouse, faPlus, faTrashCan, faRightFromBracket)

export default FontAwesomeIcon
