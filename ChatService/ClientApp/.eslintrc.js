module.exports = {
  'env': {
    'browser': true,
    'es2021': true
  },
  'extends': [
    'eslint:recommended',
    'plugin:react/recommended',
    'plugin:@typescript-eslint/recommended'
  ],
  'parser': '@typescript-eslint/parser',
  'parserOptions': {
    'ecmaFeatures': { 'jsx': true },
    'ecmaVersion': 13,
    'sourceType': 'module'
  },
  'plugins': ['react', '@typescript-eslint'],
  'rules': {
    'indent': [
      'error',
      2,
      { SwitchCase: 1 }
    ],
    'quotes': ['error', 'single'],
    'semi': ['error', 'never'],
    'array-element-newline': ['error', { multiline: true, minItems: 3 }],
    'array-bracket-newline': ['error', { multiline: true, minItems: 3 }],
    'object-curly-spacing': ['error', 'always'],
    'object-curly-newline': ['error', { multiline: true, minProperties: 3 }],
    'max-len': ['error', { 'code': 125 }],
    'react-hooks/exhaustive-deps': 'off',
    'no-trailing-spaces': 'error',
    '@typescript-eslint/explicit-module-boundary-types': 'off',
    '@typescript-eslint/no-explicit-any': 'off',
    '@typescript-eslint/no-empty-interface': 'off',
    '@typescript-eslint/ban-ts-comment': 'off',
    '@typescript-eslint/no-empty-function': 'off',
    'react/jsx-max-props-per-line': ['error', { 'maximum': { 'single': 3, 'multi': 1 } }],
    'react/jsx-closing-bracket-location': ['error', 'tag-aligned'],
    'react/jsx-space-before-closing': ['error', 'always'],
  }
}
