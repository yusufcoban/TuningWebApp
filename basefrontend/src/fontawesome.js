// src/fontawesome.js
import { library } from '@fortawesome/fontawesome-svg-core'
import { FontAwesomeIcon } from '@fortawesome/vue-fontawesome'
import { faUser, faLock, faSignInAlt,faPlus,faTrashCan } from '@fortawesome/free-solid-svg-icons'

// Add icons to the library
library.add(faUser, faLock, faSignInAlt, faPlus,faTrashCan)

export default FontAwesomeIcon
