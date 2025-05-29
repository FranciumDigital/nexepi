// src/functions/index.js
import * as theme from './theme'
import * as auth from './auth'
import * as utils from './utils'

export default {
  ...theme,
  ...auth,
  ...utils
}