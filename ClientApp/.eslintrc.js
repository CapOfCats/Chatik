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
    'ecmaFeatures': {
      'jsx': true
    },
    'ecmaVersion': 13,
    'sourceType': 'module'
  },
  'plugins': [
    'react',
    '@typescript-eslint'
  ],
  'rules': {
    'indent': ['error', 2, { SwitchCase: 1 }],
    'quotes': [
      'error',
      'single'
    ],
    'semi': [
      'error',
      'never'
    ],
    'max-len': ['error', { 'code': 125 }],
    'react-hooks/exhaustive-deps': 'off',
    'no-trailing-spaces': 'error',
    'object-curly-spacing': ['error', 'always'],
    '@typescript-eslint/explicit-module-boundary-types': 'off',
    'react/jsx-max-props-per-line': ['error', { 'maximum': { 'single': 3, 'multi': 1 } }],
    'react/jsx-closing-bracket-location': ['error', 'tag-aligned']
  }
}
