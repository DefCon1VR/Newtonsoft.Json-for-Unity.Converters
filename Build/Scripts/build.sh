#!/bin/bash

# Set error flags
set -o nounset
set -o errexit
set -o pipefail

BUILD_SOLUTION=${1:-${BUILD_SOLUTION:?"Build solution required. Example: .csproj or .sln file."}}
BUILD_DESTINATION=${2:-${BUILD_DESTINATION:?"Build output path required."}}

: ${VERSION_FILE:?"File version required."}
: ${VERSION_JSON_NET:?"Json.NET version required."}
: ${VERSION_ASSEMBLY:?"Assembly version required."}
: ${VERSION_SUFFIX:?"Version suffix required."}
: ${BUILD_CONFIGURATION:="Release"}
: ${BUILD_ADDITIONAL_CONSTANTS:=}
: ${BUILD_FRAMEWORK:="netstandard2.0"}

error() {
    >&2 echo "$0: $@"
    exit 1
}

if ! [ -f "$BUILD_SOLUTION" ]
then
    error "Build solution '$BUILD_SOLUTION' not found!"
fi

echo "############"
echo "$0: BUILDING PROJECT '$BUILD_SOLUTION'"
echo "$0: BUILD $BUILD_FRAMEWORK, $BUILD_CONFIGURATION"
echo "$0: OUTPUT '$BUILD_DESTINATION'"
echo "$0: ADDITIONAL CONSTANTS '$BUILD_ADDITIONAL_CONSTANTS'"
echo "############"
echo

msbuild -t:build "$BUILD_SOLUTION" \
    -p:TargetFramework="$BUILD_FRAMEWORK" \
    -p:Configuration="$BUILD_CONFIGURATION" \
    -p:OutputPath="$BUILD_DESTINATION" \
    -p:VersionPrefix="$VERSION_UPM_NO_SUFFIX" \
    -p:VersionSuffix="$VERSION_SUFFIX" \
    -p:AssemblyVersion="$VERSION_ASSEMBLY" \
    -p:FileVersion="$VERSION_FILE" \
    -p:AdditionalConstants=\""$BUILD_ADDITIONAL_CONSTANTS"\"

echo
echo "############"
echo "$0: DONE: BUILDING PROJECT '$BUILD_SOLUTION'"
echo "$0: DONE: BUILD $BUILD_FRAMEWORK, $BUILD_CONFIGURATION"
echo "$0: DONE: OUTPUT '$BUILD_DESTINATION'"
echo "$0: ADDITIONAL CONSTANTS '$BUILD_ADDITIONAL_CONSTANTS'"
echo "############"
